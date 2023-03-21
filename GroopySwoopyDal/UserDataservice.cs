using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroopySwoopyInterfaces;
using GroopySwoopyDTO;
using MySqlConnector;

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

        public Guid LoginUser(UserDTO user)
        {
            Guid SessionID = new Guid();
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand($"SELECT id FROM user WHERE email = @email AND password = @password", con))
                    {
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@password", user.Password);

                        con.Open();
                        var reader = cmd.ExecuteReader();
                        var test = reader.Read();
                        //SessionID = new Guid(reader.GetString(8));
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
            return SessionID;
        }

        public Boolean AuthorizeUser(Guid SessionID)
        {
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand($"SELECT id FROM user WHERE session_id = @sessionID", con))
                    {
                        cmd.Parameters.AddWithValue("@sessionID", SessionID.ToString());

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
    }
}

