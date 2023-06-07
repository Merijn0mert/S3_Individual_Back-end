using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;
using Interface.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace S3_Individual_Back_end.Controllers
{

    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderContainer ordercontainer = new OrderContainer(new OrderDAL());
        ProductOrderContainer prodordercontainer = new ProductOrderContainer(new ProductOrderDAL());
        ProductContainer productcontainer = new ProductContainer(new ProductDAL());

        [HttpGet]
        public ActionResult<List<Product>> GetAllOrders(int id)
        {
            List<Order> orders = ordercontainer.GetAllUserOrder(id);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitOrder([FromForm] string products)
        {
            string cartItems = JsonSerializer.Deserialize<string>(products);
            Order order = new Order();
            order.UserID = 1;
            Order neworder = ordercontainer.CreateOrder(order);
            
            List<ProductOrder> orderproductlist = new List<ProductOrder>();

            var q = from x in cartItems.Split(",")
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
                prodorder.OrderID = neworder.OrderID;
                prodorder.Price = product.Price;
                orderproductlist.Add(prodorder);
            }
            prodordercontainer.AddProductToOrder(orderproductlist);
            return Ok();
        }
    }
}
