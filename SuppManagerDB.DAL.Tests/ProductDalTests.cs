using NUnit.Framework;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DTO;
using Microsoft.Data.SqlClient;

namespace SuppManagerDB.DAL.Tests
{
    public class ProductDalTests : DalTestBase
    {
        private ProductDal _dal;
        private SupplierDal _supplierDal;
        private CategoryDal _categoryDal;
        private int _testSupplierId;
        private int _testCategoryId;

        [SetUp]
        public void Setup()
        {
            _dal = new ProductDal();
            _supplierDal = new SupplierDal();
            _categoryDal = new CategoryDal();

            // Create test supplier
            var supplier = new Supplier
            {
                Name = "Test Supplier",
                Info = "Some info",
                Location = "Kyiv",
                Status = true
            };
            _testSupplierId = _supplierDal.Create(supplier).SupplierID;

            // Create test category via DAL (committed)
            var category = new Category { CategoryName = "Test Category" };
            _testCategoryId = _categoryDal.Create(category).CategoryID;
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up created products referencing the supplier/category
            var products = _dal.GetBySupplier(_testSupplierId);
            foreach (var p in products)
            {
                _dal.Delete(p.ProductID);
            }

            // Delete category and supplier
            if (_testCategoryId > 0)
                _categoryDal.Delete(_testCategoryId);
            if (_testSupplierId > 0)
                _supplierDal.Delete(_testSupplierId);
        }

        [Test]
        public void CreateProduct_ShouldInsertAndReturnProduct()
        {
            var product = new Product
            {
                Name = "Test Product",
                Model = "Model X",
                Number = "12345",
                Price = 199.99m,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };

            var created = _dal.Create(product);

            Assert.That(created.ProductID, Is.GreaterThan(0));
            Assert.That(created.Name, Is.EqualTo("Test Product"));
            Assert.That(created.SupplierID, Is.EqualTo(_testSupplierId));
            Assert.That(created.CategoryID, Is.EqualTo(_testCategoryId));
        }

        [Test]
        public void GetById_ShouldReturnCorrectProduct()
        {
            var product = new Product
            {
                Name = "GetById Product",
                Model = "M1",
                Number = "001",
                Price = 50m,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };
            var created = _dal.Create(product);

            var fetched = _dal.GetById(created.ProductID);

            Assert.NotNull(fetched);
            Assert.That(fetched.Name, Is.EqualTo(product.Name));
        }

        [Test]
        public void GetAll_ShouldReturnInsertedProduct()
        {
            var product = new Product
            {
                Name = "All Product",
                Model = "M2",
                Number = "002",
                Price = 75m,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };
            _dal.Create(product);

            var allProducts = _dal.GetAll();

            Assert.That(allProducts.Count, Is.GreaterThan(0));
            Assert.That(allProducts.Exists(p => p.Name == "All Product"));
        }

        [Test]
        public void UpdateProduct_ShouldModifyProduct()
        {
            var product = new Product
            {
                Name = "Update Product",
                Model = "M3",
                Number = "003",
                Price = 80m,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };
            var created = _dal.Create(product);

            created.Name = "Updated Name";
            created.Price = 90m;

            var result = _dal.Update(created);
            var updated = _dal.GetById(created.ProductID);

            Assert.IsTrue(result);
            Assert.That(updated.Name, Is.EqualTo("Updated Name"));
            Assert.That(updated.Price, Is.EqualTo(90m));
        }

        [Test]
        public void DeleteProduct_ShouldRemoveProduct()
        {
            var product = new Product
            {
                Name = "Delete Product",
                Model = "M4",
                Number = "004",
                Price = 60m,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };
            var created = _dal.Create(product);

            var deleted = _dal.Delete(created.ProductID);
            var fetched = _dal.GetById(created.ProductID);

            Assert.IsTrue(deleted);
            Assert.IsNull(fetched);
        }

        [Test]
        public void GetBySupplier_ShouldReturnCorrectProducts()
        {
            var product = new Product
            {
                Name = "Supplier Product",
                Model = "MS",
                Number = "1001",
                Price = 120m,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };
            _dal.Create(product);

            var products = _dal.GetBySupplier(_testSupplierId);

            Assert.That(products.Count, Is.GreaterThan(0));
            Assert.That(products.Exists(p => p.Name == "Supplier Product"));
        }

        [Test]
        public void GetByCategory_ShouldReturnCorrectProducts()
        {
            var product = new Product
            {
                Name = "Category Product",
                Model = "MC",
                Number = "2001",
                Price = 130m,
                SupplierID = _testSupplierId,
                CategoryID = _testCategoryId
            };
            _dal.Create(product);

            var products = _dal.GetByCategory(_testCategoryId);

            Assert.That(products.Count, Is.GreaterThan(0));
            Assert.That(products.Exists(p => p.Name == "Category Product"));
        }
    }
}
