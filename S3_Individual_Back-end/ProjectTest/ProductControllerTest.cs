using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using S3_Individual_Back_end.Controllers;
using BusinessLogic.Classes;
using BusinessLogic.Containers;
using BusinessLogic.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.IDAL;
using Interface.DTO;

namespace S3_Individual_Tests
{
    public class ProductControllerTest
    {
            [Fact]
        public async Task GetAllProducts_ReturnsOkResultWithProducts()
        {
            // Arrange
            var productContainerDALMock = new Mock<IProductContainerDAL>();
            List<ProductDTO> expectedProducts = new List<ProductDTO>
            {
                new ProductDTO { ProductID = 1, ProductName = "Product 1", Price = 10.99m },
                new ProductDTO { ProductID = 2, ProductName = "Product 2", Price = 19.99m },
                // Add more products as needed
            };
            productContainerDALMock.Setup(dal => dal.GetAllProducts()).Returns(expectedProducts);

            var productContainer = new ProductContainer(productContainerDALMock.Object);
            var controller = new ProductController(productContainer);

            // Act
            var result = await controller.GetAllProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualProducts = Assert.IsAssignableFrom<List<Product>>(okResult.Value);
            Assert.Equal(expectedProducts.Count, actualProducts.Count);
            // Assert other expectations...
        }
    }
}
