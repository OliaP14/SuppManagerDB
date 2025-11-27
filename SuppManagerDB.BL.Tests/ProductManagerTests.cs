using System;
using System.Collections.Generic;
using Moq;
using SuppManagerDB.BL.Concrete;
using SuppManagerDB.DAL.Interfaces;
using SuppManagerDB.DTO;
using Xunit;

namespace SuppManagerDB.BL.Tests
{
    public class ProductManagerTests
    {
        private readonly Mock<IProductDal> _dalMock;
        private readonly Mock<ICategoryDal> _catMock;
        private readonly Mock<IManufacturerDal> _manMock;
        private readonly ProductManager _manager;

        public ProductManagerTests()
        {
            _dalMock = new Mock<IProductDal>();
            _catMock = new Mock<ICategoryDal>();
            _manMock = new Mock<IManufacturerDal>();

            _manager = new ProductManager(_dalMock.Object, _catMock.Object, _manMock.Object);
        }

        [Fact]
        public void Create_ShouldThrow_WhenNameEmpty()
        {
            var product = new Product { Name = "", Price = 1000, SupplierID = 1 };

            Assert.Throws<Exception>(() => _manager.Create(product));
        }

        [Fact]
        public void Create_ShouldThrow_WhenPriceIsNegative()
        {
            var product = new Product { Name = "Laptop", Price = -10, SupplierID = 1 };

            Assert.Throws<Exception>(() => _manager.Create(product));
        }

        [Fact]
        public void Create_ShouldThrow_WhenSupplierIdMissing()
        {
            var product = new Product { Name = "Laptop", Price = 1000, SupplierID = 0 };

            Assert.Throws<Exception>(() => _manager.Create(product));
        }

        [Fact]
        public void Create_ShouldCallDalCreate()
        {
            var product = new Product { Name = "Asus", Price = 15000, SupplierID = 2 };

            _dalMock.Setup(d => d.Create(It.IsAny<Product>())).Returns(product);

            var result = _manager.Create(product);

            Assert.NotNull(result);
            _dalMock.Verify(d => d.Create(It.Is<Product>(p => p.Name == "Asus")), Times.Once);
        }

        [Fact]
        public void GetBySupplier_ShouldReturnFilteredProducts()
        {
            _dalMock.Setup(d => d.GetBySupplier(3)).Returns(new List<Product>
            {
                new Product { ProductID = 1, SupplierID = 3 },
                new Product { ProductID = 2, SupplierID = 3 }
            });

            var result = _manager.GetBySupplier(3);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Update_ShouldThrow_WhenIdInvalid()
        {
            var product = new Product { ProductID = 0, Name = "Test", Price = 100, SupplierID = 1 };

            Assert.Throws<Exception>(() => _manager.Update(product));
        }

        [Fact]
        public void Update_ShouldThrow_WhenNameEmpty()
        {
            var product = new Product { ProductID = 1, Name = "", Price = 100, SupplierID = 1 };

            Assert.Throws<Exception>(() => _manager.Update(product));
        }

        [Fact]
        public void Update_ShouldThrow_WhenPriceIsNegative()
        {
            var product = new Product { ProductID = 1, Name = "Test", Price = -100, SupplierID = 1 };

            Assert.Throws<Exception>(() => _manager.Update(product));
        }

        [Fact]
        public void Update_ShouldCallDalUpdate()
        {
            var product = new Product { ProductID = 1, Name = "TV", Price = 15000, SupplierID = 1 };

            _dalMock.Setup(d => d.Update(product)).Returns(true);

            var result = _manager.Update(product);

            Assert.True(result);
            _dalMock.Verify(d => d.Update(product), Times.Once);
        }

    }
}
