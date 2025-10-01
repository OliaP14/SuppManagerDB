using NUnit.Framework;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DTO;
using System.Linq;

namespace SuppManagerDB.DAL.Tests
{
    public class ManufacturerDalTests : DalTestBase
    {
        private ManufacturerDal _manufacturerDal;
        private ProductDal _productDal;
        private SupplierDal _supplierDal;
        private CategoryDal _categoryDal;
        private int _testProductId;
        private int _testSupplierId;
        private int _testCategoryId;

        [SetUp]
        public void SetUp()
        {
            _manufacturerDal = new ManufacturerDal();
            _productDal = new ProductDal();
            _supplierDal = new SupplierDal();
            _categoryDal = new CategoryDal();

            // Create test supplier
            var supplier = new Supplier
            {
                Name = "Test Supplier",
                Info = "Test Info",
                Location = "Test Location",
                Status = true
            };
            _testSupplierId = _supplierDal.Create(supplier).SupplierID;

            // Create test category
            var category = new Category
            {
                CategoryName = "Test Category"
            };
            _testCategoryId = _categoryDal.Create(category).CategoryID;

            // Create test product
            var product = new Product
            {
                Name = "Test Product",
                Model = "TM-001",
                Number = "001",
                Price = 99.99M,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };
            _testProductId = _productDal.Create(product).ProductID;
        }

        [TearDown]
        public void TearDown()
        {
            var manufacturers = _manufacturerDal.GetAll().Where(m => m.ProductID == _testProductId).ToList();
            foreach (var manufacturer in manufacturers)
            {
                _manufacturerDal.Delete(manufacturer.ManufacturerID);
            }

            if (_testProductId > 0)
                _productDal.Delete(_testProductId);

            if (_testSupplierId > 0)
                _supplierDal.Delete(_testSupplierId);

            if (_testCategoryId > 0)
                _categoryDal.Delete(_testCategoryId);
        }

        [Test]
        public void Create_ShouldInsertManufacturer()
        {
            var manufacturer = new Manufacturer
            {
                Name = "Test Manufacturer",
                Country = "Test Country",
                Website = "https://test.com",
                ProductID = _testProductId
            };

            var created = _manufacturerDal.Create(manufacturer);

            Assert.Greater(created.ManufacturerID, 0);
            Assert.AreEqual("Test Manufacturer", created.Name);
            Assert.AreEqual(_testProductId, created.ProductID);

            // Cleanup
            _manufacturerDal.Delete(created.ManufacturerID);
        }

        [Test]
        public void GetById_ShouldReturnManufacturer()
        {
            var manufacturer = new Manufacturer
            {
                Name = "GetById Manufacturer",
                Country = "Country",
                Website = "https://getbyid.com",
                ProductID = _testProductId
            };

            var created = _manufacturerDal.Create(manufacturer);

            try
            {
                var fetched = _manufacturerDal.GetById(created.ManufacturerID);
                Assert.NotNull(fetched);
                Assert.AreEqual(created.ManufacturerID, fetched.ManufacturerID);
                Assert.AreEqual("GetById Manufacturer", fetched.Name);
            }
            finally
            {
                _manufacturerDal.Delete(created.ManufacturerID);
            }
        }

        [Test]
        public void Update_ShouldModifyManufacturer()
        {
            var manufacturer = new Manufacturer
            {
                Name = "ToUpdate",
                Country = "Country",
                Website = "https://update.com",
                ProductID = _testProductId
            };

            var created = _manufacturerDal.Create(manufacturer);

            try
            {
                created.Name = "UpdatedName";
                created.Country = "UpdatedCountry";
                var result = _manufacturerDal.Update(created);

                Assert.IsTrue(result);

                var updated = _manufacturerDal.GetById(created.ManufacturerID);
                Assert.AreEqual("UpdatedName", updated.Name);
                Assert.AreEqual("UpdatedCountry", updated.Country);
            }
            finally
            {
                _manufacturerDal.Delete(created.ManufacturerID);
            }
        }

        [Test]
        public void Delete_ShouldRemoveManufacturer()
        {
            var manufacturer = new Manufacturer
            {
                Name = "ToDelete",
                Country = "Country",
                Website = "https://delete.com",
                ProductID = _testProductId
            };

            var created = _manufacturerDal.Create(manufacturer);

            var result = _manufacturerDal.Delete(created.ManufacturerID);
            Assert.IsTrue(result);

            var deleted = _manufacturerDal.GetById(created.ManufacturerID);
            Assert.IsNull(deleted);
        }

        [Test]
        public void GetAll_ShouldReturnManufacturers()
        {
            var manufacturer = new Manufacturer
            {
                Name = "GetAllTest",
                Country = "Country",
                Website = "https://getall.com",
                ProductID = _testProductId
            };

            var created = _manufacturerDal.Create(manufacturer);

            try
            {
                var manufacturers = _manufacturerDal.GetAll();
                Assert.NotNull(manufacturers);
                Assert.IsTrue(manufacturers.Any(m => m.Name == "GetAllTest"));
            }
            finally
            {
                _manufacturerDal.Delete(created.ManufacturerID);
            }
        }
    }
}