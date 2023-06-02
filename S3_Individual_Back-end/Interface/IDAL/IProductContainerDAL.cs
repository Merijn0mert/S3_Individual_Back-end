using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.DTO;


namespace Interface.IDAL
{
    public interface IProductContainerDAL
    {
        bool CreateProduct(ProductDTO productDTO);
        bool DeleteProduct(int id);
        List<ProductDTO> GetAllProducts();
        object GetByID(int id);
        ProductDTO GetProductByID(int id);
        bool UpdateProduct(ProductDTO productDTO);
    }
}
