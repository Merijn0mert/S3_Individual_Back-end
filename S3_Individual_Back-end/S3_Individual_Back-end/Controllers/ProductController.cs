using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;

namespace S3_Individual_Back_end.Controllers
{
    [ApiController]
    [Route("\"api/products\"")]
    public class ProductController : ControllerBase
    {
        ProductContainer productContainer = new ProductContainer(new ProductDAL());

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = productContainer.GetAllProducts();
            return Ok(products);
        }
    }
}
