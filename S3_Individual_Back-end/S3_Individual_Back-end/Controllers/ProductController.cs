using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;


namespace S3_Individual_Back_end.Controllers
{
    
    
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        ProductContainer productContainer = new ProductContainer(new ProductDAL());

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

        [HttpPost("product/create")]
        public async Task<IActionResult> AddProduct([FromBody]Product Product)
        {

            Product products = Product;
            productContainer.CreateProduct(products);

            return Ok();
        }
    }
}
