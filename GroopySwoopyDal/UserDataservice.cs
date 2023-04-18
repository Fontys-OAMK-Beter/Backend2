using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroopySwoopyInterfaces;
using GroopySwoopyDTO;
using MySqlConnector;
using System.Security.Cryptography.X509Certificates;

namespace GroopySwoopyDAL
{
    public class UserDataservice : IUserDataservice
    {
        public List<UserDTO> GetAllUsers()
        {
            

            List<UserDTO> users = new List<UserDTO>();

            using (MySqlConnection con = DatabaseConnection.CreateConnection())

            try
            {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM user", con))
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            UserDTO user = new UserDTO();
                            user.Id = reader.GetInt32(0);
                            user.Name = reader.GetString(1);
                            user.Password = reader.GetString(2);
                            user.Email = reader.GetString(3);



                            users.Add(user);
                        }
                    }


                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    return null;
                }
                finally
                {
                    con.Close();
                }
            return users;
        }

        public UserDTO GetUserByID(int id)
        {
            UserDTO user = new UserDTO();
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM user WHERE id = "+id, con))
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if(!reader.IsDBNull(0))
                                user.Id = reader.GetInt32(0);

                            if (!reader.IsDBNull(1))
                                user.Name = reader.GetString(1);

                            if (!reader.IsDBNull(2))
                                user.Password = reader.GetString(2);

                            if (!reader.IsDBNull(3))
                                user.Email = reader.GetString(3);
                        }
                    }


                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    return null;
                }
                finally
                {
                    con.Close();
                }
            return user;
        }

        public void Post(UserDTO user)
        {
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO user (id, name, password, email) VALUES (@id, @name, @password, @email)", con))
                    {

                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@name", user.Name);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@email", user.Email);

                        con.Open();
                        var reader = cmd.ExecuteReader();
                    }


                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
                finally
                {
                    con.Close();
                }
        }

        public UserDTO VerifyLoginCredentials(UserDTO _user)
        {
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand($"SELECT (id, name) FROM user WHERE email = @email AND password = @password", con))
                    {
                        cmd.Parameters.AddWithValue("@email", _user.Email);
                        cmd.Parameters.AddWithValue("@password", _user.Password);

                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            if (!reader.IsDBNull(0))
                                _user.Id = reader.GetInt16(0);
                            if (!reader.IsDBNull(1))
                                _user.Name = reader.GetString(1);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    throw exception;
                }
                finally
                {
                    con.Close();
                }
            return _user;
        }

        public Boolean AuthorizeUser(string Token)
        {
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand($"SELECT id FROM user WHERE auth_token = @token", con))
                    {
                        cmd.Parameters.AddWithValue("@token", Token);

                        con.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                            return true;
                    }


                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    return false;
                }
                finally
                {
                    con.Close();
                }
            return false;
        }

        public Boolean SetAuthToken(int _userID, string _token)
        {
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand($"UPDATE user SET auth_token = @token WHERE id = @userID", con))
                    {
                        cmd.Parameters.AddWithValue("@userID", _userID);
                        cmd.Parameters.AddWithValue("@token", _token);

                        con.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.RecordsAffected > 0)
                            return true;
                    }


                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    throw exception;
                }
                finally
                {
                    con.Close();
                }
            return false;
        }
    }
}

