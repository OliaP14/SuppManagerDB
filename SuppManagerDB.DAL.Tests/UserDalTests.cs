using NUnit.Framework;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DTO;
using System;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Tests
{
    public class UserDalTests : DalTestBase
    {
        private UserDal _dal;

        [SetUp]
        public void Setup()
        {
            _dal = new UserDal();
        }

        [Test]
        public void CreateUser_ShouldInsertAndReturnUser()
        {
            var user = new User
            {
                UserName = "testuser",
                PasswordHash = "password123",
                Role = "Admin"
            };

            var created = _dal.Create(user);

            Assert.That(created.UserID, Is.GreaterThan(0));
            Assert.That(created.UserName, Is.EqualTo("testuser"));
            Assert.That(created.Role, Is.EqualTo("Admin"));
        }

        [Test]
        public void GetById_ShouldReturnCorrectUser()
        {
            var user = new User
            {
                UserName = "getbyiduser",
                PasswordHash = "pass",
                Role = "User"
            };

            var created = _dal.Create(user);
            var fetched = _dal.GetById(created.UserID);

            Assert.IsNotNull(fetched);
            Assert.That(fetched.UserID, Is.EqualTo(created.UserID));
            Assert.That(fetched.UserName, Is.EqualTo("getbyiduser"));
        }

        [Test]
        public void GetAll_ShouldReturnInsertedUser()
        {
            var user = new User
            {
                UserName = "getalluser",
                PasswordHash = "pass",
                Role = "User"
            };

            var created = _dal.Create(user);
            var allUsers = _dal.GetAll();

            Assert.That(allUsers.Exists(u => u.UserID == created.UserID));
        }

        [Test]
        public void UpdateUser_ShouldModifyUser()
        {
            var user = new User
            {
                UserName = "updatetest",
                PasswordHash = "pass",
                Role = "User"
            };

            var created = _dal.Create(user);

            created.UserName = "updatedname";
            created.Role = "Admin";
            var result = _dal.Update(created);

            Assert.IsTrue(result);

            var fetched = _dal.GetById(created.UserID);
            Assert.That(fetched.UserName, Is.EqualTo("updatedname"));
            Assert.That(fetched.Role, Is.EqualTo("Admin"));
        }

        [Test]
        public void DeleteUser_ShouldRemoveUser()
        {
            var user = new User
            {
                UserName = "deletetest",
                PasswordHash = "pass",
                Role = "User"
            };

            var created = _dal.Create(user);
            var result = _dal.Delete(created.UserID);

            Assert.IsTrue(result);

            var fetched = _dal.GetById(created.UserID);
            Assert.IsNull(fetched);
        }
    }
}
