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

        public ProductDTO GetProductByID(int id)
        {
            ProductDTO productDTO = default;

            try
            {
                const string sql = "SELECT * FROM [product] WHERE productid = @ID";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            productDTO = new ProductDTO()
                            {
                                ProductID = (int)reader["productid"],
                                ProductName = (string)reader["name"],
                                Price = (decimal)reader["price"],
                                Description = (string)reader["productdescription"],
                                ProductImage = (byte[])reader["productimage"]
                            };
                        }
                    }

                    return productDTO;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Invalid SQL query. ", ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Invalid arguments. ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Foutmelding. ", ex);
            }
        }
        public bool CreateProduct(ProductDTO prodDTO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
                try
                {
                    conn.Open();
                    string query = "INSERT INTO product(name, price, productdescription, productimage)" +
                                   "VALUES (@productname, @price, @productdescription, @productimage)";
                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@productname", prodDTO.ProductName);
                    command.Parameters.AddWithValue("@price", prodDTO.Price);
                    command.Parameters.AddWithValue("@productdescription", (prodDTO.Description == null ? "" : prodDTO.Description));
                    command.Parameters.AddWithValue("@productimage", prodDTO.ProductImage);

                    command.ExecuteNonQuery();

                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
        }
    }
}
