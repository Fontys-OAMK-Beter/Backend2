using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroopySwoopyInterfaces;
using GroopySwoopyDTO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;

namespace GroopySwoopyDAL
{
    public class UserDataservice : IUserDataservice
    {
        public List<UserDTO> GetAllUsers()
        {
            

            List<UserDTO> users = new List<UserDTO>();

            using (SqlConnection con = DatabaseConnection.CreateConnection())

            try
            {
                    using (SqlCommand cmd = new SqlCommand("SELECT id, name, password, email, picture_url, role FROM [user]", con))
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            UserDTO user = new UserDTO();
                            if (!reader.IsDBNull(0))
                                user.Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                user.Name = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                                user.Password = reader.GetString(2);
                            if (!reader.IsDBNull(3))
                                user.Email = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                user.PictureUrl = reader.GetString(4);
                            if (!reader.IsDBNull(5))
                                user.Role = reader.GetString(5);



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
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT id, name, password, email, picture_url, role FROM [user] WHERE id = " + id, con))
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                user.Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                user.Name = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                                user.Password = reader.GetString(2);
                            if (!reader.IsDBNull(3))
                                user.Email = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                user.PictureUrl = reader.GetString(4);
                            if (!reader.IsDBNull(5))
                                user.Role = reader.GetString(5);
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

        public void DeleteUserByID(int id)
        {
            UserDTO user = new UserDTO();
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM [user] WHERE id = " + id, con))
                    {
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


        public void UpdateUser(UserDTO user)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE [user] SET name = @name, password = @password, email = @email WHERE id = @id", con))
                    {

                        cmd.Parameters.AddWithValue("@name", user.Name);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@id", user.Id);
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

        public void Post(UserDTO user)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                 {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [user] (name, password, email) VALUES (@name, @password, @email)", con))
                    {
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
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand($"SELECT id, name FROM [user] WHERE email = @email AND password = @password", con))
                    {
                        cmd.Parameters.AddWithValue("@email", _user.Email);
                        cmd.Parameters.AddWithValue("@password", _user.Password);

                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            if (!reader.IsDBNull(0))
                                _user.Id = reader.GetInt32(0);
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
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand($"SELECT id FROM [user] WHERE auth_token = @token", con))
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
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand($"UPDATE [user] SET auth_token = @token WHERE id = @userID", con))
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


        public List<PartyDTO> GetPartiesByUserId(int UserId)
        {
            List<PartyDTO> party = new List<PartyDTO>();

            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT id, picture_url, title FROM [party] WHERE id = (SELECT party_id FROM [partyuser] WHERE user_id = @user_id)", con))
                    //using (SqlCommand cmd = new SqlCommand("SELECT party_id FROM [partyuser] WHERE user_id = @user_id", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", UserId);
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            party.Add(new PartyDTO());
                            if (!reader.IsDBNull(0))
                                party.LastOrDefault().Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                party.LastOrDefault().PictureURL = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                                party.LastOrDefault().Title = reader.GetString(2);
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
            return party;
        }
    }
}

