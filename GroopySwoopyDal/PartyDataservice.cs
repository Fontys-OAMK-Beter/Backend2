using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyDAL
{
    public class PartyDataservice : IPartyDataservice
    {
        public void Post(PartyDTO party, int UserId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO party(title,picture_url) VALUES(@title,@picture_url); DECLARE @party_id int; SET @party_id = SCOPE_IDENTITY(); INSERT INTO [partyuser](user_id, party_id, partymanager) VALUES(@user_id, @party_id, 1)", con))
                    {
                        cmd.Parameters.AddWithValue("@title", party.Title);
                        cmd.Parameters.AddWithValue("@picture_url", party.PictureURL);
                        cmd.Parameters.AddWithValue("@user_id", UserId);

                        cmd.ExecuteNonQuery();
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

        public void RemoveUser(int UserId, int PartyId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM partyuser WHERE id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", UserId);
                    cmd.ExecuteNonQuery();


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

        public void AddUser(string email, int PartyId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DECLARE @UserID int; SET @UserID = (SELECT id FROM [user] WHERE email = @email); INSERT INTO partyuser(user_id,party_Id) VALUES(@UserID,@party_Id)", con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@party_Id", PartyId);

                        cmd.ExecuteNonQuery();
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


        public void PromoteUser(int UserId, int PartyId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE (user_id,party_Id) VALUES(@user_id,@party_Id)", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", UserId);
                        cmd.Parameters.AddWithValue("@party_Id", PartyId);

                        cmd.ExecuteNonQuery();
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

        public PartyDTO GetPartyById(int PartyId)
        {
            PartyDTO party = new PartyDTO();

            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM party WHERE id = @party_id", con))
                    {
                        cmd.Parameters.AddWithValue("@party_id", PartyId);
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            party.Id = reader.GetInt32(0);
                            party.PictureURL= reader.GetString(1);
                            party.Title = reader.GetString(2);
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

        public List<UserDTO> GetUsersByPartyID(int PartyId)
        {
            List<UserDTO> users = new List<UserDTO>();

            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT id, name, email, role, picture_url FROM [user] WHERE id = (SELECT user_id FROM [partyuser] WHERE party_id = @party_id)", con))
                    {
                        cmd.Parameters.AddWithValue("@party_id", PartyId);
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            users.Add(new UserDTO());

                            if (!reader.IsDBNull(0))
                                users.First().Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                users.First().Name = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                                users.First().Email = reader.GetString(2);
                            if (!reader.IsDBNull(3))
                                users.First().Role = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                users.First().PictureUrl = reader.GetString(4);
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
    }
}
