using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
    public class Product
    {
        public int ProductID { get; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] ProductImage { get; set; }

        public Product(int productid, string productname, decimal price, string description, byte[] image)
        {
            this.ProductID = productid;
            this.ProductName = productname;
            this.Price = price;
            this.Description = description;
            this.ProductImage = image;
        }

        public Product(int id)
        {
            ProductID = id;
        }
        public Product()
        {

        }
    }
}
