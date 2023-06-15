using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.DTO;
using Interface.IDAL;
using Npgsql;

namespace DataAccess.DAL
{
    public class ProductDAL : IProductContainerDAL
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=S3_kaarsen;Username=postgres;";

        public List<ProductDTO> GetAllProducts()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            try
            {
                const string query = "SELECT * FROM public.product";


                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        connection.Open();

                        NpgsqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ProductDTO product = new ProductDTO()
                            {
                                ProductID = (int)reader["productid"],
                                ProductName = (string)reader["productname"],
                                Price = (decimal)reader["productprice"],
                                Description = (string)reader["productdescription"],
                                ProductImage = (byte[])reader["productimage"]
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
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
                const string query = "SELECT * FROM public.product WHERE productid = @ID";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@ID", id);

                        NpgsqlDataReader reader = command.ExecuteReader();
                        

                        while (reader.Read())
                        {
                            productDTO = new ProductDTO()
                            {
                                ProductID = (int)reader["productid"],
                                ProductName = (string)reader["productname"],
                                Price = (decimal)reader["productprice"],
                                Description = (string)reader["productdescription"],
                                ProductImage = (byte[])reader["productimage"]
                            };
                        }
                    }

                    return productDTO;
                }
            }
            catch (NpgsqlException ex)
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
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                try
                {
                    connection.Open();
                    string query = "INSERT INTO public.product(productname, productprice, productdescription, productimage)" +
                                   "VALUES (@productname, @productprice, @productdescription, @productimage)";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);

                    command.Parameters.AddWithValue("@productname", prodDTO.ProductName);
                    command.Parameters.AddWithValue("@productprice", prodDTO.Price);
                    command.Parameters.AddWithValue("@productdescription", (prodDTO.Description == null ? "" : prodDTO.Description));
                    command.Parameters.AddWithValue("@productimage", prodDTO.ProductImage);

                    command.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
        }

        public bool UpdateProduct(ProductDTO prodDTO)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                try
                {
                    connection.Open();
                    string sql = "UPDATE public.product SET productname = @productname, productprice = @productprice, productdescription = @productdescription WHERE productid = @productid";
                    NpgsqlCommand command = new NpgsqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@productid", prodDTO.ProductID);
                    command.Parameters.AddWithValue("@productname", prodDTO.ProductName);
                    command.Parameters.AddWithValue("@productprice", prodDTO.Price);
                    command.Parameters.AddWithValue("@productdescription", prodDTO.Description);


                    command.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
        }
        public bool DeleteProduct(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                try
                {
                    connection.Open();
                    string query = "DELETE FROM public.product WHERE productid = @productid";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);

                    command.Parameters.AddWithValue("@productid", id);


                    command.ExecuteNonQuery();

                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
        }
        public ProductDTO GetByID(int id)
        {
            ProductDTO productDTO = default;

            try
            {
                const string query = "SELECT * FROM public.product WHERE productid = @ID";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@ID", id);

                        NpgsqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            productDTO = new ProductDTO()
                            {
                                ProductID = (int)reader["productid"],
                                ProductName = (string)reader["productname"],
                                Price = (decimal)reader["productprice"],
                                Description = (string)reader["productdescription"],
                                ProductImage = (byte[])reader["productimage"]
                            };
                        }
                    }

                    return productDTO;
                }
            }
            catch (NpgsqlException ex)
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
    }
}
