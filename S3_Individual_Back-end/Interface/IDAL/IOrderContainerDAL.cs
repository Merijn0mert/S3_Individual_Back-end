using KaarsenInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaarsenInterface.IDAL
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
