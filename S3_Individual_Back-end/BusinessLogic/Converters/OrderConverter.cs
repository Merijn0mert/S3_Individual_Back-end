﻿using KaarsenInterface.DTO;
using KaarsenLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaarsenLogic.Converter
{
    public static class OrderConverter
    {
        public static OrderDTO toDTO(this Order Model)
        {
            OrderDTO orderdto = new OrderDTO
            (
                Model.OrderID,
                Model.UserID,
                Model.OrderDate
            );

            return orderdto;
        }

        public static Order toModel(this OrderDTO DTO)
        {
            Order order = new Order
            (
                DTO.OrderID,
                DTO.UserID,
                DTO.OrderDate
            );

            return order;
        }
    }
}
