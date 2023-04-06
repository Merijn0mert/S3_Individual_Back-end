using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.IDAL;
using Interface.DTO;
using BusinessLogic.Classes;
using BusinessLogic.Converters;

namespace BusinessLogic.Containers
{
    public class ProductContainer
    {
        public IProductContainerDAL productContainerDAL { get; }

        public ProductContainer(IProductContainerDAL dal)
        {
            this.productContainerDAL = dal;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();


            foreach (ProductDTO productdto in productContainerDAL.GetAllProducts())
            {
                products.Add(productdto.toModel());
            }

            return products;
        }
    }
}
