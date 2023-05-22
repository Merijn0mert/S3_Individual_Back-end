using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
    public class Order
    {
        public int OrderID { get; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }

        public Order (int orderID, int userID, DateTime date)
        {
            this.OrderID = orderID;
            this.UserID = userID;
            this.OrderDate = date;
        }
        public Order(int id)
        {
            OrderID = id;
        }
    }
}
