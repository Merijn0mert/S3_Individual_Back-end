using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.DTO;


namespace Interface.IDAL
{
    public interface IOrderContainerDAL
    {
        List<OrderDTO> GetAll();
        OrderDTO GetByID(OrderDTO orderdto);
        OrderDTO CreateOrder(OrderDTO orderdto);
        bool DeleteOrder(int id);
        List<OrderDTO> GetAllUserOrder(int id);
    }
}
