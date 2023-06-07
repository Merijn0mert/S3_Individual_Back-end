using Interface.DTO;
using Interface.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataAccess.DAL
{
    // TODO: Refactor naar UserDTO
    public class UserDAL : IUserContainerDAL
    {
        private string _connectionString = "Server=mssqlstud.fhict.local;Database=dbi432217_kaarsen;User Id=dbi432217_kaarsen;Password=kaarsen;";

        public bool CreateUser(UserDTO userdto)
        {
            

                try
                {
                    const string sqlQuery = "SELECT COUNT(email) FROM [user] WHERE email = @Email";

                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        int inUse;
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            connection.Open();

                            command.Parameters.AddWithValue("@Email", userdto.Email.ToLower().Trim());

                            inUse = (int)command.ExecuteScalar(); // Eerste resultaat, negeer de rest
                        }

                        if (inUse >= 1) // Meerdere resultaten
                        {
                            throw new Exception($"Email already exists! {userdto.Email}");
                        }
                    }                
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO [user](email, name, surname, password, rolid)" +
                                   "VALUES (@email, @name, @surname, HASHBYTES('SHA2_256', @password), @rolid)";
                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@email", userdto.Email);
                    command.Parameters.AddWithValue("@name", userdto.Name);
                    command.Parameters.AddWithValue("@surname", userdto.SurName);
                    command.Parameters.AddWithValue("@password", userdto.Password);
                    command.Parameters.AddWithValue("@rolid", userdto.Rolid);

                    command.ExecuteNonQuery();

                    conn.Close();
                    return true;
                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
        }
        public List<UserDTO> GetAll()
        {
            List<UserDTO> users = new List<UserDTO>();

            try
            {
                const string sql = "SELECT * FROM [user]";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            UserDTO user = new UserDTO()
                            {
                                UserID = (int)reader["userid"],
                                Email = (string)reader["email"],
                                SurName = (string)reader["surname"],
                                Name = (string)reader["name"],
                                //Password = "",                                
                                Rolid = (int)reader["rolid"]
                            };

                            users.Add(user);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("There was an SQL error during GetAll() users.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an issue retrieving all users.", ex);
            }

            return users;
        }

        public UserDTO GetByID(int id)
        {
            UserDTO userDTO = default;

            try
            {
                const string sql = "SELECT * FROM [user] WHERE userid = @ID";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@ID", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            userDTO = new UserDTO()
                            {
                                UserID = (int)reader["userid"],
                                SurName = (string)reader["surname"],
                                Name = (string)reader["name"],
                                //Password = "",
                                Email = (string)reader["email"],
                                Rolid = (int)reader["rolid"]
                            };
                        }
                    }

                    return userDTO;
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
        //public UserDTO AttemptLogin(UserDTO userDto)
        public UserDTO AttemptLogin(string email, string password)
        {
            UserDTO userDTO = default;
            try
            {
                //string _email = userDto.Email.ToLower().Trim();
                string _email = email.ToLower().Trim();
                //string _password = userDto.Password.Trim();
                string _password = password.Trim();

                const string query =
                    "SELECT * FROM [user] WHERE email = @email AND password=HASHBYTES('SHA2_256', @password)";
                //"SELECT * FROM [user] WHERE email = @email AND password = @Password";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand commandDatabase = new SqlCommand(query, connection))
                    {
                        commandDatabase.Parameters.AddWithValue("@email", _email);
                        commandDatabase.Parameters.AddWithValue("@password", _password);

                        try
                        {
                            connection.Open();

                            SqlDataReader reader = commandDatabase.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    userDTO = new UserDTO
                                    {
                                        UserID = (int)reader["userid"],
                                        Name = (string)reader["name"],
                                        SurName = (string)reader["surname"],
                                        Email = (string)reader["email"],
                                        Rolid = (int)reader["rolid"]
                                    };
                                }
                            }
                            else
                            {
                                throw new Exception("Incorrect credentials");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message + ex.StackTrace);
                            Console.Write("Connection Error", "Information");
                        }
                    }

                    return userDTO;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + ex.StackTrace);
                throw new Exception("Foutmelding.", ex);
            }
        }

        //public bool Register(string firstName, string lastName, string _email, int _type, string _password)

        public bool Register(UserDTO userDto)
        {
            try
            {
                const string sqlQuery = "SELECT COUNT(email) FROM [user] WHERE email = @Email";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    int inUse;
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@Email", userDto.Email.ToLower().Trim());

                        inUse = (int)command.ExecuteScalar(); // Eerste resultaat, negeer de rest
                    }

                    if (inUse >= 1) // Meerdere resultaten
                    {
                        throw new Exception($"Email already exists! {userDto.Email}");
                    }
                }

                const string sqlInsert = "INSERT INTO [user] (name, surname, email, password, rolid) VALUES (@name, @surname, @email, HASHBYTES('SHA2_256', @password), @rolid)";
                //"INSERT INTO [user] (name, username, email, password, rolid) VALUES (@name, @username, @email, @password, @rolid)";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@name", userDto.Name);
                        command.Parameters.AddWithValue("@surname", userDto.SurName);
                        command.Parameters.AddWithValue("@email", userDto.Email);
                        command.Parameters.AddWithValue("@password", userDto.Password);
                        command.Parameters.AddWithValue("@rolid", userDto.Rolid);

                        try
                        {
                            connection.Open();

                            command.ExecuteNonQuery();
                        }

                        catch (SqlException ex)
                        {
                            throw new Exception("Error inserting SQL into database.", ex);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error inserting SQL.", ex);
                        }
                    }
                }
            }

            catch (SqlException ex)
            {
                throw new Exception("SQL Error querying info from database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error querying info from database.", ex);
            }

            return true;
        }

        public bool ForgotPassword(string email, string password)
        {
            try
            {
                // Make a sql query to retrieve all users where email is the same as what is filled in
                const string sql = "UPDATE [user] SET password = HASHBYTES('SHA2_256', @Password) WHERE email = @Email";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        var Executecommand = command.ExecuteNonQuery();
                        if (Executecommand >= 1)
                        {
                            return true;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        // REFACTOR
        public bool ChangePassword(string Password, int userID)
        {
            try
            {
                const string sql = "UPDATE [user] SET password = HASHBYTES('SHA2_256', @Password) WHERE userid = @UserID";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@UserID", userID);

                        try
                        {
                            command.ExecuteNonQuery();
                        }

                        catch (Exception ex)
                        {
                            throw new Exception("Geen rows affected.", ex);
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Kan geen verbinding maken met SQL query.", ex); }
        }
    }
}