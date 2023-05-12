﻿using System;
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
        List<ProductDTO> GetAllProducts();
        ProductDTO GetProductByID(int id);
    }
}
