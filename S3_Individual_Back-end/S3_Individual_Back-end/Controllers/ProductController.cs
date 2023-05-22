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
        public ActionResult<List<Product>> GetAllProducts()
        {
            List<Product> products = productContainer.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("product/{id}")]
        public IActionResult GetAllProductByID(int id)
        {

            Product product = productContainer.GetProductByID(id);

            return Ok(product);
        }

        [HttpPost("product/create")]
        public async Task<IActionResult> AddProduct([FromBody]Product Product)
        {
            /*if (postedFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    postedFile.CopyTo(ms);
                    Product.ProductImage = ms.ToArray();
                    // act on the Base64 data
                }
            }*/

            Product products = Product;
            productContainer.CreateProduct(products);

            return Ok();
        }
    }
}
