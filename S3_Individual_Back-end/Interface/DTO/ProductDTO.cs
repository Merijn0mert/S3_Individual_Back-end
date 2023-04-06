using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public ProductDTO(int productid, string productname, decimal price, string description)
        {
            this.ProductID = productid;
            this.ProductName = productname;
            this.Price = price;
            this.Description = description;
        }

        public ProductDTO()
        {

        }
    }
}
