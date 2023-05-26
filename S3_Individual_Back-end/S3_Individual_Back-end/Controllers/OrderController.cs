using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;
using Microsoft.AspNetCore.Mvc;

namespace S3_Individual_Back_end.Controllers
{

    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderContainer orderContainer = new OrderContainer(new OrderDAL());

       [HttpGet]
        public ActionResult<List<Product>> GetAllOrders(int id)
        {
            List<Order> orders = orderContainer.GetAllUserOrder(id);
            return Ok(orders);
        }
    }
}
