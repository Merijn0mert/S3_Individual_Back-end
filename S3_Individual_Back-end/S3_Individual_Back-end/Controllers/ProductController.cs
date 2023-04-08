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
        public ActionResult<List<Product>> GetAllProducts()
        {
            List<Product> products = productContainer.GetAllProducts();
            return Ok(products);
        }
    }
}
