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
    public class MovieDataservice : IMovieDataservice
    {
        public void Post()
        {
            //using (SqlConnection con = DatabaseConnection.CreateConnection())

            //    try
            //    {
            //        con.Open();
            //        using (SqlCommand cmd = new SqlCommand("INSERT INTO ", con))
            //        {
            //            cmd.Parameters.AddWithValue("@", );
            //            cmd.Parameters.AddWithValue("@" ,);

            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine(exception.ToString());
            //    }
            //    finally
            //    {
            //        con.Close();
            //    }
        }

        public void RemoveMovie()
        {
            //using (SqlConnection con = DatabaseConnection.CreateConnection())

            //    try
            //    {
            //        con.Open();
            //        SqlCommand cmd = new SqlCommand("DELETE FROM  WHERE id=@Id", con);
            //        cmd.Parameters.AddWithValue("@", );
            //        cmd.ExecuteNonQuery();


            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine(exception.ToString());
            //    }
            //    finally
            //    {
            //        con.Close();
            //    }
        }

        public void AddMovie()
        {
            //using (SqlConnection con = DatabaseConnection.CreateConnection())

            //    try
            //    {
            //        con.Open();
            //        using (SqlCommand cmd = new SqlCommand("INSERT INTO ", con))
            //        {
            //            cmd.Parameters.AddWithValue("@", );
            //            cmd.Parameters.AddWithValue("@", );

            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine(exception.ToString());
            //    }
            //    finally
            //    {
            //        con.Close();
            //    }
        }

        public void UpdateMovie()
        {
            //using (SqlConnection con = DatabaseConnection.CreateConnection())

            //    try
            //    {
            //        con.Open();
            //        using (SqlCommand cmd = new SqlCommand("UPDATE (user_id,party_Id) VALUES(@user_id,@party_Id)", con))
            //        {
            //            cmd.Parameters.AddWithValue("@", );
            //            cmd.Parameters.AddWithValue("@", );

            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine(exception.ToString());
            //    }
            //    finally
            //    {
            //        con.Close();
            //    }
        }

        public MovieDTO GetMovie()
        {
            //MovieDTO movie = new MovieDTO();
            //using (SqlConnection con = DatabaseConnection.CreateConnection())

            //    try
            //    {
            //        using (SqlCommand cmd = new SqlCommand("SELECT * FROM movie", con))
            //        {
            //            con.Open();
            //            var reader = cmd.ExecuteReader();
            //            while (reader.Read())
            //            {
            //               movie.Id = reader.GetInt32(0);

            //               movie.Votes = reader.GetInt32(1);
            //            }
            //        }


            //    }
            //    catch (Exception exception)
            //    {
            //        Console.WriteLine(exception.ToString());
            //        return null;
            //    }
            //    finally
            //    {
            //        con.Close();
            //    }


            //return movie;
            return null;
        }
    }
}
