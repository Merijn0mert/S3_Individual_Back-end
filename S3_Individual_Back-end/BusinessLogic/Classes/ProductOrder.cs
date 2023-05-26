using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
    public class ProductOrder
    {
        public int OrderID { get; }
        public int ProductID { get; }
        public int Quantity { get; }
        public decimal Price { get; set; }

        public ProductOrder(int orderid, int productid, int quantity, decimal price)
        {
            this.OrderID = orderid;
            this.ProductID = productid;
            this.Quantity = quantity;
            this.Price = price;
        }
    }
}
