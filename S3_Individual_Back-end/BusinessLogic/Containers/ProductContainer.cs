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

        public Product GetProductByID(int id)
        {
            Product products = productContainerDAL.GetProductByID(id).toModel();

            return products;
        }

        public bool CreateProduct(Product product)
        {
            return productContainerDAL.CreateProduct(product.toDTO());
        }

        public bool UpdateProduct(Product product)
        {
            return productContainerDAL.UpdateProduct(product.toDTO());
        }
        public bool DeleteProduct(int id)
        {
            return (productContainerDAL.DeleteProduct(id));
        }

        public Product GetByID(int id)
        {
            Product products = productContainerDAL.GetByID(id).toModel();

            return products;
        }
    }
}
