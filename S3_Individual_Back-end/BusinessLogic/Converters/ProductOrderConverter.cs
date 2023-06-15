using BusinessLogic.Classes;
using Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Converters
{
    public static class ProductOrderConverter
    {
        public static ProductOrderDTO toDTO(this ProductOrder Model)
        {
            ProductOrderDTO prodorderdto = new ProductOrderDTO
            (
                Model.OrderID,
                Model.ProductID,
                Model.Quantity,
                Model.Price
            );

            return prodorderdto;
        }

        public static ProductOrder toModel(this ProductOrderDTO DTO)
        {
            ProductOrder prodorder = new ProductOrder
            (
                DTO.OrderID,
                DTO.ProductID,
                DTO.Quantity,
                DTO.Price
            );

            return prodorder;
        }
    }
}
