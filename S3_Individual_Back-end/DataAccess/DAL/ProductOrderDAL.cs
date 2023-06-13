using Interface.DTO;
using Interface.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;

namespace DataAccess.DAL
{
    public class ProductOrderDAL : IProductOrderContainerDAL
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=S3_kaarsen;Username=postgres;";

        public bool AddProductToOrder(ProductOrderDTO prodorderDTO)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                try
                {
                    conn.Open();
                    string query = "INSERT INTO productorder(orderid, productid, productcount, productprice)" +
                        "VALUES (@orderid, @productid, @productcount, @productprice)";
                    NpgsqlCommand command = new NpgsqlCommand(query, conn);

                    command.Parameters.AddWithValue("@orderid", prodorderDTO.OrderID);
                    command.Parameters.AddWithValue("@productid", prodorderDTO.ProductID);
                    command.Parameters.AddWithValue("@productcount", prodorderDTO.Quantity);
                    command.Parameters.AddWithValue("@productprice", prodorderDTO.Price);

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
       
        public List<ProductDTO> GetAllOrderProduct(int id)
        {
            List<ProductDTO> products = new List<ProductDTO>();
            try
            {

                const string sql = "SELECT product.productid, product.name, product.price, product.productdescription, product.productimage, [order].orderid FROM [productorder]" +
                             " INNER JOIN [order] ON productorder.orderid = [order].orderid " +
                             " INNER JOIN product ON productorder.productid = product.productid" +
                             " WHERE [order].orderid = @ID";

                
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ID", id);

                        NpgsqlDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            ProductDTO product = new ProductDTO()
                            {
                                ProductID = (int)reader["productid"],
                                ProductName = (string)reader["name"],
                                Price = (decimal)reader["price"],
                                Description = (string)reader["productdescription"],
                                //ProductImage = (byte[])reader["productimage"],
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

        public List<ProductDTO> GetAll()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            try
            {
                const string sql = "SELECT * FROM [product]";


                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        connection.Open();

                        NpgsqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ProductDTO product = new ProductDTO()
                            {
                                ProductID = (int)reader["productid"],
                                ProductName = (string)reader["name"],
                                Price = (decimal)reader["price"],
                                Description = (string)reader["productdescription"]
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
    }
}
