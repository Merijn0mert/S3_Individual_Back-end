using Interface.DTO;
using Interface.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data.SqlClient;

namespace DataAccess.DAL
{
    public class OrderDAL : IOrderContainerDAL
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=S3_kaarsen;Username=postgres;";

        public OrderDTO CreateOrder(OrderDTO orderdto)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                try
                {
                    conn.Open();
                    string query = "INSERT INTO public.order(userid, orderdate)" +
                                   "VALUES (@userid, CURRENT_DATE);" +
                                   "SELECT LASTVAL();";
                    NpgsqlCommand command = new NpgsqlCommand(query, conn);
                    
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
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                try
                {
                    conn.Open();
                    string query = "DELETE FROM public.order WHERE orderid = @orderid";
                    NpgsqlCommand command = new NpgsqlCommand(query, conn);

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
                const string sql = "SELECT * FROM public.order";


                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        connection.Open();

                        NpgsqlDataReader reader = command.ExecuteReader();

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
            catch (NpgsqlException ex)
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
                const string sql = "SELECT * FROM public.order WHERE orderid = @ID";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@ID", orderdto.OrderID);

                        NpgsqlDataReader reader = command.ExecuteReader();

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
        public List<OrderDTO> GetAllUserOrder(int id)
        {
            List<OrderDTO> orders = new List<OrderDTO>();

            try
            {
                const string sql = "SELECT * FROM public.order WHERE userid = @ID";


                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ID", id);

                        NpgsqlDataReader reader = command.ExecuteReader();

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
            catch (NpgsqlException ex)
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
