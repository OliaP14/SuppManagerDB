using System;
using System.Collections.Generic;
using Moq;
using SuppManagerDB.BL.Concrete;
using SuppManagerDB.DAL.Interfaces;
using SuppManagerDB.DTO;
using Xunit;

namespace SuppManagerDB.BL.Tests
{
    public class SupplierManagerTests
    {
        private readonly Mock<ISupplierDal> _dalMock;
        private readonly SupplierManager _manager;

        public SupplierManagerTests()
        {
            _dalMock = new Mock<ISupplierDal>();
            _manager = new SupplierManager(_dalMock.Object);
        }

        [Fact]
        public void Create_ShouldThrow_WhenNameIsEmpty()
        {
            var supplier = new Supplier { Name = "", Info = "some info" };

            Assert.Throws<Exception>(() => _manager.Create(supplier));
        }

        [Fact]
        public void Create_ShouldThrow_WhenInfoIsEmpty()
        {
            var supplier = new Supplier { Name = "Samsung", Info = "" };

            Assert.Throws<Exception>(() => _manager.Create(supplier));
        }

        [Fact]
        public void Create_ShouldSetStatusTrue()
        {
            var supplier = new Supplier { Name = "LG", Info = "support@mail.com" };

            _dalMock.Setup(d => d.Create(It.IsAny<Supplier>()))
                    .Returns((Supplier s) => s);

            var result = _manager.Create(supplier);

            Assert.True(result.Status);
        }

        [Fact]
        public void Update_ShouldThrow_WhenIdInvalid()
        {
            var supplier = new Supplier { SupplierID = 0, Name = "Xiaomi", Info = "info" };

            Assert.Throws<Exception>(() => _manager.Update(supplier));
        }

        [Fact]
        public void Search_ReturnsFilteredResults()
        {
            // Arrange
            _dalMock.Setup(d => d.GetAll()).Returns(new List<Supplier>
            {
                new Supplier { Name = "Samsung", Info = "123", Location = "Kyiv" },
                new Supplier { Name = "Apple", Info = "456", Location = "Lviv" }
            });

            // Act
            var result = _manager.Search("sam");

            // Assert
            Assert.Single(result); // only Samsung
        }

        [Fact]
        public void Search_EmptyText_ReturnsAll()
        {
            _dalMock.Setup(d => d.GetAll()).Returns(new List<Supplier>
            {
                new Supplier { Name = "Sony", Info = "phone", Location = "Odessa" }
            });

            var result = _manager.Search("");

            Assert.Single(result);
        }

        [Fact]
        public void SetStatus_ShouldReturnFalse_IfSupplierNotFound()
        {
            _dalMock.Setup(d => d.GetById(It.IsAny<int>())).Returns((Supplier)null);

            var result = _manager.SetStatus(99, false);

            Assert.False(result);
        }

        [Fact]
        public void SetStatus_ShouldCallUpdate()
        {
            var supplier = new Supplier { SupplierID = 3, Name = "Dell", Status = true };

            _dalMock.Setup(d => d.GetById(3)).Returns(supplier);
            _dalMock.Setup(d => d.UpdateSupplierStatus(supplier)).Returns(true);

            var result = _manager.SetStatus(3, false);

            Assert.True(result);
            _dalMock.Verify(d => d.UpdateSupplierStatus(supplier), Times.Once);
        }
    }
}
