﻿using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyDAL
{
    public class EventDataservice : IEventDataservice
    {
        public void Delete(int id)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM event WHERE id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
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

        public List<EventDTO> GetAllEventsByGroupId(int id)
        {
            List<EventDTO> events = new List<EventDTO>();

            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM event WHERE party_id = " + id, con))
                    {
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            EventDTO @event = new EventDTO();
                            if (!reader.IsDBNull(0))
                                @event.Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                @event.StartTime = reader.GetDateTime(1);
                            if (!reader.IsDBNull(2))
                                @event.Description = reader.GetString(2);
                            if (!reader.IsDBNull(3))
                                @event.Title = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                                @event.GroupId = reader.GetInt32(4);
                            if (!reader.IsDBNull(5))
                                @event.PictureUrl = reader.GetString(5);



                            events.Add(@event);
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
            return events;
        }

        public EventDTO GetSpecificGroupEvent(int EventId, int GroupId)
        {
            EventDTO @event = new EventDTO();
            using (SqlConnection con = DatabaseConnection.CreateConnection())
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM event WHERE (party_id = @party_id AND id = @id)", con))
                    {
                        cmd.Parameters.AddWithValue("@id", EventId);
                        cmd.Parameters.AddWithValue("@party_id", GroupId);

                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                @event.Id = reader.GetInt32(0);

                            if (!reader.IsDBNull(1))
                                @event.StartTime = reader.GetDateTime(1);

                            if (!reader.IsDBNull(2))
                                @event.Description = reader.GetString(2);

                            if (!reader.IsDBNull(3))
                                @event.Title = reader.GetString(3);

                            if (!reader.IsDBNull(2))
                                @event.GroupId = reader.GetInt32(4);

                            if (!reader.IsDBNull(3))
                                @event.PictureUrl = reader.GetString(5);
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
            return @event;
        }

        public void Post(EventDTO eventDTO)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())

                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO event(start_time, Description,Title,party_id,picture_url) VALUES(@start_time, @Description,@Title,@party_id, @picture_url)", con))
                    {
                        cmd.Parameters.AddWithValue("@start_time", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Description", eventDTO.Description);
                        cmd.Parameters.AddWithValue("@Title", eventDTO.Title);
                        cmd.Parameters.AddWithValue("@party_id", eventDTO.GroupId);
                        cmd.Parameters.AddWithValue("@picture_url", eventDTO.PictureUrl);

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
