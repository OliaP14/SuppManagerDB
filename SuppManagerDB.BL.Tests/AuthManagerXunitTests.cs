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
    public class AuthManagerXunitTests
    {
        private readonly Mock<IUserDal> _userDalMock;
        private readonly Mock<IUserPrivilegeDal> _privDalMock;
        private readonly AuthManager _manager;

        public AuthManagerXunitTests()
        {
            _userDalMock = new Mock<IUserDal>();
            _privDalMock = new Mock<IUserPrivilegeDal>();
            _manager = new AuthManager(_userDalMock.Object, _privDalMock.Object);
        }

        private static byte[] ComputeHashForTest(string password, Guid salt)
        {
            using var sha = SHA256.Create();
            string combined = password + salt.ToString().ToUpper();
            byte[] bytes = Encoding.Unicode.GetBytes(combined);
            return sha.ComputeHash(bytes);
        }

        [Fact]
        public void Login_ReturnsNull_WhenUserNotFound()
        {
            _userDalMock.Setup(d => d.GetByLogin("missing")).Returns((User)null);

            var result = _manager.Login("missing", "any");

            Assert.Null(result);
        }

        [Fact]
        public void Login_ReturnsNull_WhenPasswordIncorrect()
        {
            var salt = Guid.NewGuid();
            // stored hash for password "correct"
            var storedHash = ComputeHashForTest("correct", salt);

            var user = new User
            {
                UserID = 1,
                Login = "user1",
                PasswordHash = storedHash,
                Salt = salt
            };

            _userDalMock.Setup(d => d.GetByLogin("user1")).Returns(user);

            var result = _manager.Login("user1", "wrong");

            Assert.Null(result);
        }

        [Fact]
        public void Login_ReturnsUserAndPopulatesPrivileges_WhenCredentialsCorrect()
        {
            var salt = Guid.NewGuid();
            var password = "p@ssw0rd";
            var storedHash = ComputeHashForTest(password, salt);

            var user = new User
            {
                UserID = 5,
                Login = "admin",
                PasswordHash = storedHash,
                Salt = salt
            };

            var privileges = new List<Privilege>
            {
                new Privilege { PrivilegeID = 1, Name = "Admin" },
                new Privilege { PrivilegeID = 2, Name = "Manager" }
            };

            _userDalMock.Setup(d => d.GetByLogin("admin")).Returns(user);
            _privDalMock.Setup(d => d.GetPrivilegesForUser(5)).Returns(privileges);

            var result = _manager.Login("admin", password);

            Assert.NotNull(result);
            Assert.Equal(5, result.UserID);
            Assert.NotNull(result.Privileges);
            Assert.Equal(2, result.Privileges.Count);
            Assert.Contains(result.Privileges, p => p.Name == "Admin");
        }

        [Fact]
        public void HasPrivilege_ReturnsTrue_WhenPrivilegeExists()
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
        public void HasPrivilege_ReturnsFalse_WhenPrivilegeMissing()
        {
            var userId = 11;
            _privDalMock.Setup(d => d.GetPrivilegesForUser(userId)).Returns(new List<Privilege>());

            var result = _manager.HasPrivilege(userId, "Admin");

            Assert.False(result);
        }
    }
}
