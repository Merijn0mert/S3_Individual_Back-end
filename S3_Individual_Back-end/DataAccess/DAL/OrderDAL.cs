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
    public class OrderDAL : IOrderContainerDAL
    {
        private string _connectionString = "Server=mssqlstud.fhict.local;Database=dbi432217_kaarsen;User Id=dbi432217_kaarsen;Password=kaarsen;";

        public OrderDTO CreateOrder(OrderDTO orderdto)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
                try
                {
                    conn.Open();
                    string query = "INSERT INTO [order](userid, orderdate)" +
                                   "VALUES (@userid,getdate());" +
                                   "SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, conn);
                    
                    command.Parameters.AddWithValue("@userid", orderdto.UserID);

                   
                    int g = Convert.ToInt32(command.ExecuteScalar());
                    OrderDTO order = new OrderDTO();                   
                    order.OrderID = g;

                    return order;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
        }
        public bool DeleteOrder(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
                try
                {
                    conn.Open();
                    string query = "DELETE FROM [order] WHERE orderid = @orderid";
                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@orderid", id);


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
        
        public List<OrderDTO> GetAll()
        {
            List<OrderDTO> orders = new List<OrderDTO>();

            try
            {
                const string sql = "SELECT * FROM [order]";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            OrderDTO order = new OrderDTO()
                            {
                                OrderID = (int)reader["orderid"],
                                UserID = (int)reader["userid"],
                                OrderDate = (DateTime)reader["orderdate"]
                            };

                            orders.Add(order);
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

            return orders;
        }

        public OrderDTO GetByID(OrderDTO orderdto)
        {
            OrderDTO orderDTO = default;

            try
            {
                const string sql = "SELECT * FROM [order] WHERE orderid = @ID";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@ID", orderdto.OrderID);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            orderDTO = new OrderDTO()
                            {
                                OrderID = (int)reader["orderid"],
                                UserID = (int)reader["userid"],
                                OrderDate = (DateTime)reader["orderdate"]
                            };
                        }
                    }

                    return orderDTO;
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
        public List<OrderDTO> GetAllUserOrder(int id)
        {
            List<OrderDTO> orders = new List<OrderDTO>();

            try
            {
                const string sql = "SELECT * FROM [order] WHERE userid = @ID";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            OrderDTO order = new OrderDTO()
                            {
                                OrderID = (int)reader["orderid"],
                                UserID = (int)reader["userid"],
                                OrderDate = (DateTime)reader["orderdate"]
                            };

                            orders.Add(order);
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

            return orders;
        }
    }
}
