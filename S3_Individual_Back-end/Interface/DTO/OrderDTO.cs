using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }

        public OrderDTO(int orderID, int userID, DateTime date)
        {
            this.OrderID = orderID;
            this.UserID = userID;
            this.OrderDate = date;
        }

        public OrderDTO()
        {

        }
    }
}
