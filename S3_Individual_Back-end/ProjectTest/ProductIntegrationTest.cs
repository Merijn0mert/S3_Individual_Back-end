using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Net.Http.Json;
using S3_Individual_Back_end;
using Interface.DTO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace S3_Individual_Tests
{
    public class ProductControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkResult()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<ProductDTO[]>(content);

            // Assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetProductByID_ReturnsOkResult()
        {
            // Arrange
            int productId = 1;

            // Act
            var response = await _client.GetAsync($"/api/products/{productId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(content);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(productId, product.ProductID);
        }

        [Fact]
        public async Task AddProduct_ReturnsOkResult()
        {
            // Arrange
            var newProduct = new ProductDTO
            {
                ProductName = "New Product",
                Price = 9.99m
                // Set other properties as needed
            };
            var json = JsonConvert.SerializeObject(newProduct);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/products", content);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkResult()
        {
            // Arrange
            int productId = 1;

            // Act
            var response = await _client.PostAsync($"/api/products/delete?id={productId}", null);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
