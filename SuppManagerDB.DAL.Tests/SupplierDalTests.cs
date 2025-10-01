using NUnit.Framework;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DTO;
using System;
using System.Collections.Generic;

namespace SuppManagerDB.DAL.Tests
{
    public class SupplierDalTests : DalTestBase
    {
        private SupplierDal _dal;

        [SetUp]
        public void Setup()
        {
            _dal = new SupplierDal();
        }

        [Test]
        public void CreateSupplier_ShouldInsertAndReturnSupplier()
        {
            var supplier = new Supplier
            {
                Name = "Test Supplier",
                Info = "Some info",
                Location = "Kyiv",
                Status = true
            };

            var created = _dal.Create(supplier);

            Assert.That(created.SupplierID, Is.GreaterThan(0));
            Assert.That(created.Name, Is.EqualTo("Test Supplier"));
            Assert.That(created.Status, Is.True);
        }

        [Test]
        public void GetById_ShouldReturnCorrectSupplier()
        {
            var supplier = new Supplier
            {
                Name = "GetById Supplier",
                Info = "Info",
                Location = "Lviv",
                Status = true
            };

            var created = _dal.Create(supplier);
            var fetched = _dal.GetById(created.SupplierID);

            Assert.IsNotNull(fetched);
            Assert.That(fetched.SupplierID, Is.EqualTo(created.SupplierID));
            Assert.That(fetched.Name, Is.EqualTo("GetById Supplier"));
        }

        [Test]
        public void GetAll_ShouldReturnInsertedSupplier()
        {
            var supplier = new Supplier
            {
                Name = "GetAll Supplier",
                Info = "Info",
                Location = "Odesa",
                Status = false
            };

            var created = _dal.Create(supplier);
            var allSuppliers = _dal.GetAll();

            Assert.That(allSuppliers.Exists(s => s.SupplierID == created.SupplierID));
        }

        [Test]
        public void UpdateSupplier_ShouldModifySupplier()
        {
            var supplier = new Supplier
            {
                Name = "Update Supplier",
                Info = "Info",
                Location = "Dnipro",
                Status = true
            };

            var created = _dal.Create(supplier);

            created.Name = "Updated Name";
            created.Status = false;

            var result = _dal.Update(created);

            Assert.IsTrue(result);

            var fetched = _dal.GetById(created.SupplierID);
            Assert.That(fetched.Name, Is.EqualTo("Updated Name"));
            Assert.That(fetched.Status, Is.False);
        }

        [Test]
        public void DeleteSupplier_ShouldRemoveSupplier()
        {
            var supplier = new Supplier
            {
                Name = "Delete Supplier",
                Info = "Info",
                Location = "Kharkiv",
                Status = true
            };

            var created = _dal.Create(supplier);
            var result = _dal.Delete(created.SupplierID);

            Assert.IsTrue(result);

            var fetched = _dal.GetById(created.SupplierID);
            Assert.IsNull(fetched);
        }

        [Test]
        public void UpdateSupplierStatus_ShouldChangeStatus()
        {
            var supplier = new Supplier
            {
                Name = "Status Supplier",
                Info = "Info",
                Location = "Vinnytsia",
                Status = true
            };

            var created = _dal.Create(supplier);

            created.Status = false;
            var result = _dal.UpdateSupplierStatus(created);

            Assert.IsTrue(result);

            var fetched = _dal.GetById(created.SupplierID);
            Assert.That(fetched.Status, Is.False);
        }
    }
}
