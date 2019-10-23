using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyRetail.Rest.Controllers;
using MyRetail.Rest.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace MyRetail.Rest.Tests.Controller
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void GetProduct_ShouldReturnById()
        {
            //Act
            var controller = new ProductsController();
            IHttpActionResult actionResult = controller.Get(13860428);
            var contentResult = actionResult as OkNegotiatedContentResult<Product>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.Content.Id, 13860428);
            Assert.AreEqual(contentResult.Content.Name, "The Big Lebowski (Blu-ray)");
            Assert.AreEqual(contentResult.Content.CurrentPrice.Value, 11.00M);
            Assert.AreEqual(contentResult.Content.CurrentPrice.CurrencyCode, "USD");

        }

        [TestMethod]
        public void GetProduct_BadRequest_If_ProductIs_Is_Zero()
        {
            // Arrange
            var id = 0;

            var controller = new ProductsController();

            // Act
            var result = controller.Get(id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void GetProduct_BadRequest()
        {
            // Arrange
            var id = 12;

            var controller = new ProductsController();

            // Act
            var result = controller.Get(id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }


        [TestMethod]
        public void UpdateProductPrice_Success()
        {
            // Arrange
            var id = 15117729;
            var price = 15.70M;

            var controller = new ProductsController();

            // Act
            IHttpActionResult actionResult = controller.Put(id, price);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateProductPrice_BadRequest()
        {
            // Arrange
            var id = 0;
            var price = 15.70M;

            var controller = new ProductsController();

            // Act
            var result = controller.Put(id, price);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }
    }
}