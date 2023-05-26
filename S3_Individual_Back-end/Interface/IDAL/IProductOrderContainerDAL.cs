using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.DTO;


namespace Interface.IDAL
{
    public interface IProductOrderContainerDAL
    {
        List<ProductDTO> GetAllOrderProduct(int id);
        bool AddProductToOrder(ProductOrderDTO prodorderDTO);
    }
}
