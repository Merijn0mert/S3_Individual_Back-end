﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;


namespace S3_Individual_Back_end.Controllers
{
    
    
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContainer productContainer;

        public ProductController(ProductContainer productContainer)
        {
            this.productContainer = productContainer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = productContainer.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetProductByID(int id)
        {

            Product product = productContainer.GetProductByID(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts([FromForm]Product Product)
        {

            productContainer.CreateProduct(Product);

            return Ok();
        }

        [HttpPost("/delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            productContainer.DeleteProduct(id);
            return RedirectToAction("ViewAdminProduct");
        }
    }
}
