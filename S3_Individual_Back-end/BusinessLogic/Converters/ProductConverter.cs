using BusinessLogic.Classes;
using Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Converters
{
    public static class ProductConverter
    {
        public static ProductDTO toDTO(this Product Model)
        {
            ProductDTO productdto = new ProductDTO
            (
                Model.ProductID,
                Model.ProductName,
                Model.Price,
                Model.Description,
                Model.ProductImage
            );

            return productdto;
        }

        public static Product toModel(this ProductDTO DTO)
        {
            Product product = new Product
            (
                DTO.ProductID,
                DTO.ProductName,
                DTO.Price,
                DTO.Description,
                DTO.ProductImage
            );

            return product;
        }
    }
}

