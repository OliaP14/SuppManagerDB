using NUnit.Framework;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DTO;
using System;
using System.Linq;

namespace SuppManagerDB.DAL.Tests
{
    public class ContractDalTests : DalTestBase
    {
        private ContractDal _contractDal;
        private SupplierDal _supplierDal;
        private UserDal _userDal;
        private int _testSupplierId;
        private int _testUserId;
        private Contract _testContract;

        [SetUp]
        public void SetUp()
        {
            _contractDal = new ContractDal();
            _supplierDal = new SupplierDal();
            _userDal = new UserDal();

            // Create test supplier
            var supplier = new Supplier
            {
                Name = "Test Supplier",
                Info = "Test Info",
                Location = "Test Location",
                Status = true
            };
            var createdSupplier = _supplierDal.Create(supplier);
            _testSupplierId = createdSupplier.SupplierID;

            // Create test user
            var user = new User
            {
                UserName = "TestUser",
                PasswordHash = "TestHash",
                Role = "TestRole"
            };
            var createdUser = _userDal.Create(user);
            _testUserId = createdUser.UserID;

            _testContract = new Contract
            {
                SupplierID = _testSupplierId,
                ContractNumber = "CN-001",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1),
                UserID = _testUserId
            };
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up contracts for the test supplier
            var contracts = _contractDal.GetBySupplier(_testSupplierId);
            foreach (var contract in contracts)
            {
                _contractDal.Delete(contract.ContractID);
            }

            // Clean up test user
            if (_testUserId > 0)
                _userDal.Delete(_testUserId);

            // Clean up test supplier
            if (_testSupplierId > 0)
                _supplierDal.Delete(_testSupplierId);
        }

        [Test]
        public void Create_ValidContract_ReturnsContractWithId()
        {
            var result = _contractDal.Create(_testContract);

            try
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ContractID, Is.GreaterThan(0));
                Assert.That(result.SupplierID, Is.EqualTo(_testContract.SupplierID));
                Assert.That(result.ContractNumber, Is.EqualTo(_testContract.ContractNumber));
            }
            finally
            {
                if (result?.ContractID > 0)
                    _contractDal.Delete(result.ContractID);
            }
        }

        [Test]
        public void GetById_ExistingContract_ReturnsContract()
        {
            var created = _contractDal.Create(_testContract);

            try
            {
                var result = _contractDal.GetById(created.ContractID);

                Assert.That(result, Is.Not.Null);
                Assert.That(result.ContractID, Is.EqualTo(created.ContractID));
                Assert.That(result.ContractNumber, Is.EqualTo(_testContract.ContractNumber));
            }
            finally
            {
                _contractDal.Delete(created.ContractID);
            }
        }

        [Test]
        public void GetById_NonExistingContract_ReturnsNull()
        {
            var result = _contractDal.GetById(-1);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Update_ExistingContract_ReturnsTrue()
        {
            var created = _contractDal.Create(_testContract);

            try
            {
                created.ContractNumber = "UpdatedCN";
                created.EndDate = DateTime.Today.AddYears(2);

                var updateResult = _contractDal.Update(created);
                var updated = _contractDal.GetById(created.ContractID);

                Assert.That(updateResult, Is.True);
                Assert.That(updated.ContractNumber, Is.EqualTo("UpdatedCN"));
                Assert.That(updated.EndDate, Is.EqualTo(created.EndDate));
            }
            finally
            {
                _contractDal.Delete(created.ContractID);
            }
        }

        [Test]
        public void Delete_ExistingContract_ReturnsTrue()
        {
            var created = _contractDal.Create(_testContract);

            var deleteResult = _contractDal.Delete(created.ContractID);
            var deleted = _contractDal.GetById(created.ContractID);

            Assert.That(deleteResult, Is.True);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Delete_NonExistingContract_ReturnsFalse()
        {
            var result = _contractDal.Delete(-1);
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetAll_ReturnsAllContracts()
        {
            var contract1 = _contractDal.Create(_testContract);
            var contract2 = _contractDal.Create(new Contract
            {
                SupplierID = _testSupplierId,
                ContractNumber = "CN-002",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1),
                UserID = _testUserId 
            });

            try
            {
                var results = _contractDal.GetAll();

                Assert.That(results, Is.Not.Null);
                Assert.That(results.Count, Is.GreaterThanOrEqualTo(2));
                Assert.That(results.Any(c => c.ContractID == contract1.ContractID));
                Assert.That(results.Any(c => c.ContractID == contract2.ContractID));
            }
            finally
            {
                _contractDal.Delete(contract1.ContractID);
                _contractDal.Delete(contract2.ContractID);
            }
        }

        [Test]
        public void GetBySupplier_ReturnsCorrectContracts()
        {
            var contract1 = _contractDal.Create(_testContract);
            var contract2 = _contractDal.Create(new Contract
            {
                SupplierID = _testSupplierId,
                ContractNumber = "CN-003",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1),
                UserID = _testUserId 
            });

            try
            {
                var results = _contractDal.GetBySupplier(_testSupplierId);

                Assert.That(results, Is.Not.Null);
                Assert.That(results.Count, Is.GreaterThanOrEqualTo(2));
                Assert.That(results.All(c => c.SupplierID == _testSupplierId));
            }
            finally
            {
                _contractDal.Delete(contract1.ContractID);
                _contractDal.Delete(contract2.ContractID);
            }
        }
    }
}