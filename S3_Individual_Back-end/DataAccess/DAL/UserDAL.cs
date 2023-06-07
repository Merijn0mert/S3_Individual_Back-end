using Interface.DTO;
using Interface.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DataAccess.DAL
{
    // TODO: Refactor naar UserDTO
    public class UserDAL : IUserContainerDAL
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=S3_kaarsen;Username=postgres;";

        public bool CreateUser(UserDTO userdto)
        {
            

                try
                {
                    const string sqlQuery = "SELECT COUNT(useremail) FROM public.user WHERE useremail = @Email";

                    using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                    {
                    long inUse;
                        using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                        {
                            connection.Open();

                            command.Parameters.AddWithValue("@Email", userdto.Email.ToLower().Trim());

                            inUse = (long)command.ExecuteScalar(); // Eerste resultaat, negeer de rest
                        }

                        if (inUse >= 1) // Meerdere resultaten
                        {
                            throw new Exception($"Email already exists! {userdto.Email}");
                        }
                    }                
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    string password = userdto.Password; // Retrieve the password from the userdto object

                    // Convert the string to a byte array
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                    string query = "INSERT INTO public.user(useremail, firstname, lastname, userpassword, rolid)" +
                                   "VALUES (@useremail, @firstname, @lastname, @userpassword, @rolid)";

                    NpgsqlCommand command = new NpgsqlCommand(query, conn);

                    command.Parameters.AddWithValue("@useremail", userdto.Email);
                    command.Parameters.AddWithValue("@firstname", userdto.Name);
                    command.Parameters.AddWithValue("@lastname", userdto.SurName);
                    command.Parameters.AddWithValue("@userpassword", passwordBytes); // Pass the byte array value
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
                const string sql = "SELECT * FROM public.user";


                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        connection.Open();

                        NpgsqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            UserDTO user = new UserDTO()
                            {
                                UserID = (int)reader["userid"],
                                Email = (string)reader["useremail"],
                                SurName = (string)reader["firstname"],
                                Name = (string)reader["lastname"],
                                //Password = "",                                
                                Rolid = (int)reader["rolid"]
                            };

                            users.Add(user);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
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
                const string sql = "SELECT * FROM public.user WHERE userid = @ID";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@ID", id);

                        NpgsqlDataReader reader = command.ExecuteReader();

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
                    "SELECT * FROM public.user WHERE useremail = @email AND userpassword=HASHBYTES('SHA2_256', @password)";
                //"SELECT * FROM [user] WHERE email = @email AND password = @Password";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand commandDatabase = new NpgsqlCommand(query, connection))
                    {
                        commandDatabase.Parameters.AddWithValue("@email", _email);
                        commandDatabase.Parameters.AddWithValue("@password", _password);

                        try
                        {
                            connection.Open();

                            NpgsqlDataReader reader = commandDatabase.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    userDTO = new UserDTO
                                    {
                                        UserID = (int)reader["userid"],
                                        Email = (string)reader["useremail"],
                                        SurName = (string)reader["firstname"],
                                        Name = (string)reader["lastname"],                               
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
                const string sqlQuery = "SELECT COUNT(useremail) FROM public.user WHERE useremail = @Email";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    int inUse;
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
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

                const string sqlInsert = "INSERT INTO public.user (firstname, lastname, useremail, userpassword, rolid) VALUES (@name, @surname, @email, HASHBYTES('SHA2_256', @password), @rolid)";
                //"INSERT INTO [user] (name, username, email, password, rolid) VALUES (@name, @username, @email, @password, @rolid)";
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlInsert, connection))
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

                        catch (NpgsqlException ex)
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

            catch (NpgsqlException ex)
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
                const string sql = "UPDATE public.user SET userpassword = HASHBYTES('SHA2_256', @Password) WHERE useremail = @Email";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
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
                const string sql = "UPDATE public.user SET userpassword = HASHBYTES('SHA2_256', @Password) WHERE userid = @UserID";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
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