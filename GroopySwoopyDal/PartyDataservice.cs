using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using Microsoft.Data.SqlClient;
using MySqlConnector;
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
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO party(title,picture_url) VALUES(@title,@picture_url)", con))
                    {
                        cmd.Parameters.AddWithValue("@title", party.Title);
                        cmd.Parameters.AddWithValue("@picture_url", party.PictureURL);

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

        public void AddUser(int UserId, int PartyId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO partyuser(user_id,party_Id) VALUES(@user_id,@party_Id)", con))
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

        public PartyDTO GetParty(int PartyId)
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
    }
}
