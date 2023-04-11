using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;

namespace S3_Individual_Back_end.Controllers
{
    
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductContainer productContainer = new ProductContainer(new ProductDAL());

        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            List<Product> products = productContainer.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("product/{productid}")]
        public IActionResult GetAllProductByID(int id)
        {
            id = 1;
            Product product = productContainer.GetProductByID(id);

            return Ok(product);
        }
    }
}
