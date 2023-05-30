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
    public class ProductOrderDAL : IProductOrderContainerDAL
    {
        private string _connectionString = "Server=mssqlstud.fhict.local;Database=dbi432217_kaarsen;User Id=dbi432217_kaarsen;Password=kaarsen;";

        public bool AddProductToOrder(ProductOrderDTO prodorderDTO)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
                try
                {
                    conn.Open();
                    string query = "INSERT INTO productorder(orderid, productid, productcount, productprice)" +
                        "VALUES (@orderid, @productid, @productcount, @productprice)";
                    SqlCommand command = new SqlCommand(query, conn);

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

                
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ID", id);

                        SqlDataReader reader = command.ExecuteReader();


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

        public List<ProductDTO> GetAll()
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
                                Description = (string)reader["productdescription"]
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
