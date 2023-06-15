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
    public class OrderContainer
    {
        public IOrderContainerDAL orderContainerDAL { get; }

        public OrderContainer(IOrderContainerDAL dal)
        {
            this.orderContainerDAL = dal;
        }
        public Order CreateOrder(Order order)
        {
            
            OrderDTO orderDTO = orderContainerDAL.CreateOrder(order.toDTO());
            {

                Order orderss = orderDTO.toModel();
                return orderss;
            }
        }
        public bool DeleteOrder(int id)
        {
            return orderContainerDAL.DeleteOrder(id);
        }

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();


            foreach (OrderDTO orderdto in orderContainerDAL.GetAll())
            {
                orders.Add(orderdto.toModel());
            }

            return orders;
        }

        public List<Order> GetAllUserOrder(int id)
        {
            List<Order> orders = new List<Order>();
            foreach(OrderDTO orderdto in orderContainerDAL.GetAllUserOrder(id))
            {
                orders.Add(orderdto.toModel());
            }
            return orders;
        }

        public Order GetByID(Order order)
        {
            OrderDTO orderdto = new OrderDTO();
            order = orderContainerDAL.GetByID(orderdto).toModel();

            return order;
        }
    }
}
