using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;
using Interface.DTO;
using Microsoft.AspNetCore.Mvc;

namespace S3_Individual_Back_end.Controllers
{

    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderContainer ordercontainer = new OrderContainer(new OrderDAL());
        ProductOrderContainer prodordercontainer = new ProductOrderContainer(new ProductOrderDAL());
        ProductContainer productcontainer = new ProductContainer(new ProductDAL());
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllOrders(int id)
        {
            List<Order> orders = ordercontainer.GetAllUserOrder(id);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitOrder(string productIds)
        {


                List<ProductOrder> orderproductlist = new List<ProductOrder>();

                var q = from x in productIds.Split(",")
                        group x by x into g
                        let count = g.Count()
                        orderby count descending
                        select new { Value = g.Key, Count = count };
                foreach (var item in q)
                {
                ProductOrder prodorder = new ProductOrder();
                var product = productcontainer.GetByID(Convert.ToInt32(item.Value));
                prodorder.ProductID = Convert.ToInt32(item.Value);
                prodorder.Quantity = item.Count;
                prodorder.OrderID = (int)HttpContext.Session.GetInt32("OrderID");
                prodorder.Price = product.Price;
                    orderproductlist.Add(prodorder);
                }
            return Ok();
        }
    }
}
