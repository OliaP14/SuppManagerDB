using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Moq;
using SuppManagerDB.BL.Concrete;
using SuppManagerDB.DAL.Interfaces;
using SuppManagerDB.DTO;
using Xunit;

namespace SuppManagerDB.BL.Tests
{
    public class AuthManagerTests
    {
        private readonly Mock<IUserDal> _userDalMock;
        private readonly Mock<IUserPrivilegeDal> _privDalMock;
        private readonly AuthManager _manager;

        public AuthManagerTests()
        {
            _userDalMock = new Mock<IUserDal>();
            _privDalMock = new Mock<IUserPrivilegeDal>();

            _manager = new AuthManager(_userDalMock.Object, _privDalMock.Object);
        }

        private static byte[] ComputeHash(string password, Guid salt)
        {
            using var sha = SHA256.Create();
            string combined = password + salt.ToString().ToUpper();
            byte[] bytes = Encoding.Unicode.GetBytes(combined);
            return sha.ComputeHash(bytes);
        }

        [Fact]
        public void Login_ReturnsNull_WhenLoginEmpty()
        {
            // If login empty, DAL should return null -> manager.Login returns null
            _userDalMock.Setup(d => d.GetByLogin(String.Empty)).Returns((User?)null);

            var result = _manager.Login(string.Empty, "1234");

            Assert.Null(result);
        }

        [Fact]
        public void Login_ReturnsNull_WhenUserNotFound()
        {
            _userDalMock.Setup(d => d.GetByLogin("admin")).Returns((User?)null);

            var result = _manager.Login("admin", "1111");

            Assert.Null(result);
        }

        [Fact]
        public void Login_ReturnsNull_WhenPasswordWrong()
        {
            var salt = Guid.NewGuid();
            var correctHash = ComputeHash("qwerty", salt);

            var user = new User
            {
                Login = "admin",
                PasswordHash = correctHash,
                Salt = salt
            };

            _userDalMock.Setup(d => d.GetByLogin("admin")).Returns(user);

            var result = _manager.Login("admin", "wrong");

            Assert.Null(result);
        }

        [Fact]
        public void Login_ReturnsUser_IfCredentialsCorrect()
        {
            var salt = Guid.NewGuid();
            var password = "qwerty";
            var hash = ComputeHash(password, salt);

            var user = new User
            {
                Login = "admin",
                PasswordHash = hash,
                Salt = salt,
                UserID = 5
            };

            var privileges = new List<Privilege>
            {
                new Privilege { PrivilegeID = 1, Name = "Admin" }
            };

            _userDalMock.Setup(d => d.GetByLogin("admin")).Returns(user);
            _privDalMock.Setup(p => p.GetPrivilegesForUser(5)).Returns(privileges);

            var result = _manager.Login("admin", password);

            Assert.NotNull(result);
            Assert.Equal(5, result!.UserID);
            Assert.NotNull(result.Privileges);
            Assert.Contains(result.Privileges, p => p.Name == "Admin");
        }

        [Fact]
        public void HasPrivilege_ReturnsTrue_IfPrivilegeExists()
        {
            var userId = 10;
            var privileges = new List<Privilege>
            {
                new Privilege { PrivilegeID = 1, Name = "Manager" }
            };
            _privDalMock.Setup(d => d.GetPrivilegesForUser(userId)).Returns(privileges);

            var result = _manager.HasPrivilege(userId, "Manager");

            Assert.True(result);
        }

        [Fact]
        public void HasPrivilege_ReturnsFalse_IfNoPrivilege()
        {
            var userId = 10;
            _privDalMock.Setup(d => d.GetPrivilegesForUser(userId)).Returns(new List<Privilege>());

            var result = _manager.HasPrivilege(userId, "Admin");

            Assert.False(result);
        }
    }
}
