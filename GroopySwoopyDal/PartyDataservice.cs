using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
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
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO party(title,picture_url) VALUES(@title,@picture_url)", con))
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
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM partyuser WHERE id=@Id", con);
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
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO partyuser(user_id,party_Id) VALUES(@user_id,@party_Id)", con))
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
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("UPDATE (user_id,party_Id) VALUES(@user_id,@party_Id)", con))
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
    }
}
