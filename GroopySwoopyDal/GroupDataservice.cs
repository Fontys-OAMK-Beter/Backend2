using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyDAL
{
    public class GroupDataservice : IGroupDataservice
    {
        public void Post(GroupDTO group)
        {
            using (MySqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO party(title,picture_url) VALUES(@title,@picture_url)", con))
                    {
                        cmd.Parameters.AddWithValue("@title", group.Title);
                        cmd.Parameters.AddWithValue("@picture_url", group.PictureURL);

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
