using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using SuppManagerDB.WebApp.Controllers;
using SuppManagerDB.WebApp.Models;

namespace SuppManagerDB.WebApp.Tests.Controllers
{
    public class SuppliersControllerTests
    {
        private readonly Mock<ISupplierManager> _managerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<SuppliersController>> _loggerMock;

        private readonly SuppliersController _controller;

        public SuppliersControllerTests()
        {
            _managerMock = new Mock<ISupplierManager>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<SuppliersController>>();

            _controller = new SuppliersController(
                _managerMock.Object,
                _mapperMock.Object,
                _loggerMock.Object);
        }

        // Index 
        [Fact]
        public void Index_Returns_ViewResult()
        {
            // arrange
            _managerMock.Setup(m => m.GetAll())
                .Returns(new List<Supplier>());

            // act
            var result = _controller.Index();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        // Create GET 
        [Fact]
        public void Create_Get_Returns_ViewResult()
        {
            // act
            var result = _controller.Create();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        // Create POST 
        [Fact]
        public void Create_Post_ValidModel_RedirectsToIndex()
        {
            // arrange
            var model = new EditSupplierModel
            {
                Name = "Test Supplier",
                Info = "test@test.com",
                Location = "Kyiv",
                Status = true
            };

            var supplier = new Supplier();

            _mapperMock.Setup(m => m.Map<Supplier>(model))
                .Returns(supplier);

            // act
            var result = _controller.Create(model);

            // assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        // Edit GET 
        [Fact]
        public void Edit_Get_InvalidId_Returns_NotFound()
        {
            // arrange
            _managerMock.Setup(m => m.GetAll())
                .Returns(new List<Supplier>());

            // act
            var result = _controller.Edit(999);

            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Create POST - Invalid Model
        [Fact]
        public void Create_Post_InvalidModel_Returns_View()
        {
            var model = new EditSupplierModel();
            _controller.ModelState.AddModelError("Name", "Required");

            var result = _controller.Create(model);

            Assert.IsType<ViewResult>(result);
        }

    }
}


/*Index() → повертає ViewResult

Create (GET) → повертає ViewResult

Create (POST) з валідною моделлю → RedirectToAction

Edit (GET) з неіснуючим id → NotFound
*/