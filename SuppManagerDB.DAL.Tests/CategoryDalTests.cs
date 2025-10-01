using NUnit.Framework;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DTO;
using System.Linq;

namespace SuppManagerDB.DAL.Tests
{
    public class CategoryDalTests : DalTestBase
    {
        private CategoryDal _categoryDal;
        private Category _testCategory;

        [SetUp]
        public void SetUp()
        {
            _categoryDal = new CategoryDal();
            _testCategory = new Category
            {
                CategoryName = "Test Category"
            };
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up all test categories created during tests
            var categories = _categoryDal.GetAll()
                .Where(c => c.CategoryName.StartsWith("Test"))
                .ToList();

            foreach (var category in categories)
            {
                _categoryDal.Delete(category.CategoryID);
            }
        }

        [Test]
        public void Create_ValidCategory_ReturnsCategoryWithId()
        {
            // Act
            var result = _categoryDal.Create(_testCategory);

            try
            {
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.CategoryID, Is.GreaterThan(0));
                Assert.That(result.CategoryName, Is.EqualTo(_testCategory.CategoryName));
            }
            finally
            {
                // Cleanup
                if (result?.CategoryID > 0)
                    _categoryDal.Delete(result.CategoryID);
            }
        }

        [Test]
        public void GetById_ExistingCategory_ReturnsCategory()
        {
            // Arrange
            var created = _categoryDal.Create(_testCategory);

            try
            {
                // Act
                var result = _categoryDal.GetById(created.CategoryID);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.CategoryID, Is.EqualTo(created.CategoryID));
                Assert.That(result.CategoryName, Is.EqualTo(_testCategory.CategoryName));
            }
            finally
            {
                // Cleanup
                _categoryDal.Delete(created.CategoryID);
            }
        }

        [Test]
        public void GetById_NonExistingCategory_ReturnsNull()
        {
            // Act
            var result = _categoryDal.GetById(-1);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Update_ExistingCategory_ReturnsTrue()
        {
            // Arrange
            var created = _categoryDal.Create(_testCategory);
            created.CategoryName = "Updated Category Name";

            try
            {
                // Act
                var updateResult = _categoryDal.Update(created);
                var updated = _categoryDal.GetById(created.CategoryID);

                // Assert
                Assert.That(updateResult, Is.True);
                Assert.That(updated.CategoryName, Is.EqualTo("Updated Category Name"));
            }
            finally
            {
                // Cleanup
                _categoryDal.Delete(created.CategoryID);
            }
        }

        [Test]
        public void Delete_ExistingCategory_ReturnsTrue()
        {
            // Arrange
            var created = _categoryDal.Create(_testCategory);

            // Act
            var deleteResult = _categoryDal.Delete(created.CategoryID);
            var deleted = _categoryDal.GetById(created.CategoryID);

            // Assert
            Assert.That(deleteResult, Is.True);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Delete_NonExistingCategory_ReturnsFalse()
        {
            // Act
            var result = _categoryDal.Delete(-1);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetAll_ReturnsAllCategories()
        {
            // Arrange
            var category1 = _categoryDal.Create(_testCategory);
            var category2 = _categoryDal.Create(new Category
            {
                CategoryName = "Test Category 2"
            });

            try
            {
                // Act
                var results = _categoryDal.GetAll();

                // Assert
                Assert.That(results, Is.Not.Null);
                Assert.That(results.Count, Is.GreaterThanOrEqualTo(2));
                Assert.That(results.Any(c => c.CategoryID == category1.CategoryID));
                Assert.That(results.Any(c => c.CategoryID == category2.CategoryID));
            }
            finally
            {
                // Cleanup
                _categoryDal.Delete(category1.CategoryID);
                _categoryDal.Delete(category2.CategoryID);
            }
        }

        [Test]
        public void Create_DuplicateName_HandlesAppropriately()
        {
            // Arrange
            var firstCategory = _categoryDal.Create(_testCategory);

            try
            {
                // Act & Assert
                var duplicateCategory = new Category
                {
                    CategoryName = _testCategory.CategoryName
                };

                Assert.DoesNotThrow(() =>
                {
                    var result = _categoryDal.Create(duplicateCategory);
                    if (result?.CategoryID > 0)
                        _categoryDal.Delete(result.CategoryID);
                });
            }
            finally
            {
                // Cleanup
                _categoryDal.Delete(firstCategory.CategoryID);
            }
        }

        [Test]
        public void Update_NonExistingCategory_ReturnsFalse()
        {
            // Arrange
            var nonExistingCategory = new Category
            {
                CategoryID = -1,
                CategoryName = "Non-existing Category"
            };

            // Act
            var result = _categoryDal.Update(nonExistingCategory);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}