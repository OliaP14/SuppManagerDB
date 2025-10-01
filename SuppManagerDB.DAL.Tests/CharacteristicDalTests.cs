using NUnit.Framework;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DTO;
using System.Linq;

namespace SuppManagerDB.DAL.Tests
{
    public class CharacteristicDalTests : DalTestBase
    {
        private CharacteristicDal _characteristicDal;
        private ProductDal _productDal;
        private SupplierDal _supplierDal;
        private CategoryDal _categoryDal;
        private int _testProductId;
        private int _testSupplierId;
        private int _testCategoryId;
        private Characteristic _testCharacteristic;

        [SetUp]
        public void SetUp()
        {
            _characteristicDal = new CharacteristicDal();
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
            var createdSupplier = _supplierDal.Create(supplier);
            _testSupplierId = createdSupplier.SupplierID;

            // Create test category
            var category = new Category
            {
                CategoryName = "Test Category"
            };
            var createdCategory = _categoryDal.Create(category);
            _testCategoryId = createdCategory.CategoryID;

            // Create test product with numeric values for Number
            var product = new Product
            {
                Name = "Test Product",
                Model = "TM-001",
                Number = "001",
                Price = 99.99M,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };

            var createdProduct = _productDal.Create(product);
            _testProductId = createdProduct.ProductID;

            _testCharacteristic = new Characteristic
            {
                Power = 100.5f,
                Material = "Steel",
                Warranty = "2 years",
                ReleaseYear = 2023,
                ProductID = _testProductId
            };
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up test data in reverse order of creation
            var characteristics = _characteristicDal.GetByProductId(_testProductId);
            foreach (var characteristic in characteristics)
            {
                _characteristicDal.Delete(characteristic.CharacteristicID);
            }

            if (_testProductId > 0)
                _productDal.Delete(_testProductId);
            
            if (_testSupplierId > 0)
                _supplierDal.Delete(_testSupplierId);
            
            if (_testCategoryId > 0)
                _categoryDal.Delete(_testCategoryId);
        }

        [Test]
        public void Create_ValidCharacteristic_ReturnsCharacteristicWithId()
        {
            // Act
            var result = _characteristicDal.Create(_testCharacteristic);

            try
            {
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.CharacteristicID, Is.GreaterThan(0));
                Assert.That(result.Power, Is.EqualTo(_testCharacteristic.Power));
                Assert.That(result.Material, Is.EqualTo(_testCharacteristic.Material));
                Assert.That(result.Warranty, Is.EqualTo(_testCharacteristic.Warranty));
                Assert.That(result.ReleaseYear, Is.EqualTo(_testCharacteristic.ReleaseYear));
                Assert.That(result.ProductID, Is.EqualTo(_testCharacteristic.ProductID));
            }
            finally
            {
                // Cleanup
                if (result?.CharacteristicID > 0)
                    _characteristicDal.Delete(result.CharacteristicID);
            }
        }

        [Test]
        public void GetById_ExistingCharacteristic_ReturnsCharacteristic()
        {
            // Arrange
            var created = _characteristicDal.Create(_testCharacteristic);

            try
            {
                // Act
                var result = _characteristicDal.GetById(created.CharacteristicID);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.CharacteristicID, Is.EqualTo(created.CharacteristicID));
                Assert.That(result.Power, Is.EqualTo(_testCharacteristic.Power));
                Assert.That(result.Material, Is.EqualTo(_testCharacteristic.Material));
            }
            finally
            {
                // Cleanup
                _characteristicDal.Delete(created.CharacteristicID);
            }
        }

        [Test]
        public void GetById_NonExistingCharacteristic_ReturnsNull()
        {
            // Act
            var result = _characteristicDal.GetById(-1);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Update_ExistingCharacteristic_ReturnsTrue()
        {
            // Arrange
            var created = _characteristicDal.Create(_testCharacteristic);

            try
            {
                created.Material = "Updated Material";
                created.Power = 150.75f;

                // Act
                var updateResult = _characteristicDal.Update(created);
                var updated = _characteristicDal.GetById(created.CharacteristicID);

                // Assert
                Assert.That(updateResult, Is.True);
                Assert.That(updated.Material, Is.EqualTo("Updated Material"));
                Assert.That(updated.Power, Is.EqualTo(150.75f));
            }
            finally
            {
                // Cleanup
                _characteristicDal.Delete(created.CharacteristicID);
            }
        }

        [Test]
        public void Delete_ExistingCharacteristic_ReturnsTrue()
        {
            // Arrange
            var created = _characteristicDal.Create(_testCharacteristic);

            // Act
            var deleteResult = _characteristicDal.Delete(created.CharacteristicID);
            var deleted = _characteristicDal.GetById(created.CharacteristicID);

            // Assert
            Assert.That(deleteResult, Is.True);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void GetAll_ReturnsAllCharacteristics()
        {
            // Arrange
            var characteristic1 = _characteristicDal.Create(_testCharacteristic);
            var characteristic2 = _characteristicDal.Create(new Characteristic
            {
                Power = 200.0f,
                Material = "Aluminum",
                Warranty = "3 years",
                ReleaseYear = 2024,
                ProductID = _testProductId
            });

            try
            {
                // Act
                var results = _characteristicDal.GetAll();

                // Assert
                Assert.That(results, Is.Not.Null);
                Assert.That(results.Count, Is.GreaterThanOrEqualTo(2));
                Assert.That(results.Any(c => c.CharacteristicID == characteristic1.CharacteristicID));
                Assert.That(results.Any(c => c.CharacteristicID == characteristic2.CharacteristicID));
            }
            finally
            {
                // Cleanup
                _characteristicDal.Delete(characteristic1.CharacteristicID);
                _characteristicDal.Delete(characteristic2.CharacteristicID);
            }
        }

        [Test]
        public void GetByProductId_ReturnsCorrectCharacteristics()
        {
            // Arrange
            var characteristic1 = _characteristicDal.Create(_testCharacteristic);
            var characteristic2 = _characteristicDal.Create(new Characteristic
            {
                Power = 200.0f,
                Material = "Aluminum",
                Warranty = "3 years",
                ReleaseYear = 2024,
                ProductID = _testProductId
            });

            try
            {
                // Act
                var results = _characteristicDal.GetByProductId(_testProductId);

                // Assert
                Assert.That(results, Is.Not.Null);
                Assert.That(results.Count, Is.EqualTo(2));
                Assert.That(results.All(c => c.ProductID == _testProductId));
            }
            finally
            {
                // Cleanup
                _characteristicDal.Delete(characteristic1.CharacteristicID);
                _characteristicDal.Delete(characteristic2.CharacteristicID);
            }
        }

        [Test]
        public void GetByProduct_ReturnsCorrectCharacteristics()
        {
            // Arrange
            var characteristic1 = _characteristicDal.Create(_testCharacteristic);
            var characteristic2 = _characteristicDal.Create(new Characteristic
            {
                Power = 200.0f,
                Material = "Aluminum",
                Warranty = "3 years",
                ReleaseYear = 2024,
                ProductID = _testProductId
            });

            try
            {
                // Act
                var results = _characteristicDal.GetByProduct(_testProductId);

                // Assert
                Assert.That(results, Is.Not.Null);
                Assert.That(results.Count, Is.EqualTo(2));
                Assert.That(results.All(c => c.ProductID == _testProductId));
            }
            finally
            {
                // Cleanup
                _characteristicDal.Delete(characteristic1.CharacteristicID);
                _characteristicDal.Delete(characteristic2.CharacteristicID);
            }
        }
    }
}