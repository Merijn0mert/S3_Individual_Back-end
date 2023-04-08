using Interface.DTO;
using Interface.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess.DAL
{
    public class ProductDAL : IProductContainerDAL
    {
        private string _connectionString = "Server=mssqlstud.fhict.local;Database=dbi432217_kaarsen;User Id=dbi432217_kaarsen;Password=kaarsen;";

        public List<ProductDTO> GetAllProducts()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            try
            {
                const string sql = "SELECT * FROM [product]";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ProductDTO product = new ProductDTO()
                            {
                                ProductID = (int)reader["productid"],
                                ProductName = (string)reader["name"],
                                Price = (decimal)reader["price"],
                                Description = (string)reader["productdescription"],
                                ProductImage = (byte[])reader["productimage"]
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("There was an SQL error during GetAll() product.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an issue retrieving all product.", ex);
            }

            return products;
        }
    }
}
