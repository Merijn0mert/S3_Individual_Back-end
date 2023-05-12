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

        [HttpGet("product/{id}")]
        public IActionResult GetAllProductByID(int id)
        {

            Product product = productContainer.GetProductByID(id);

            return Ok(product);
        }

        [HttpPost("product/create")]
        public IActionResult AddProduct(Product product, IFormFile postedFile)
        {

            {
                if (product == null)
                {
                    return BadRequest("Product data is null.");
                }
                if (postedFile != null && postedFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        postedFile.CopyTo(ms);
                        product.ProductImage = ms.ToArray();
                        // act on the Base64 data
                    }
                }

                productContainer.CreateProduct(product);

                return Ok();
            }
        }
    }
}
