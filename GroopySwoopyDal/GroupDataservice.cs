using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace GroopySwoopyDAL
{
    public class GroupDataservice : IGroupDataservice
    {
        public void CreateGroup(GroupDTO group, int adminId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [group](name, admin_id) VALUES(@name, @admin_id); DECLARE @group_id int; SET @group_id = SCOPE_IDENTITY();", con))
                    {
                        cmd.Parameters.AddWithValue("@name", group.Name);
                        cmd.Parameters.AddWithValue("@admin_id", adminId);
                        cmd.ExecuteNonQuery();

                        // Insert group members
                        foreach (int memberId in group.MemberIDs)
                        {
                            AddGroupMember(group.Id, memberId);
                        }
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

        public void AddGroupMember(int groupId, int memberId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO groupmember(group_id, member_id) VALUES(@group_id, @member_id)", con))
                    {
                        cmd.Parameters.AddWithValue("@group_id", groupId);
                        cmd.Parameters.AddWithValue("@member_id", memberId);
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

        public void RemoveGroupMember(int groupId, int memberId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM groupmember WHERE group_id = @group_id AND member_id = @member_id", con))
                    {
                        cmd.Parameters.AddWithValue("@group_id", groupId);
                        cmd.Parameters.AddWithValue("@member_id", memberId);
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

        public void PromoteGroupMember(int groupId, int memberId)
        {
            using (SqlConnection con = DatabaseConnection.CreateConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE groupmember SET is_admin = 1 WHERE group_id = @group_id AND member_id = @member_id", con))
                    {
                        cmd.Parameters.AddWithValue("@group_id", groupId);
                        cmd.Parameters.AddWithValue("@member_id", memberId);
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

        public GroupDTO GetGroupById(int groupId)
        {
            GroupDTO group = new GroupDTO();

            using (SqlConnection con = DatabaseConnection.CreateConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM [group] WHERE id = @group_id", con))
                    {
                        cmd.Parameters.AddWithValue("@group_id", groupId);
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                group.Id = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                                group.Name = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                                group.AdminID = reader.GetInt32(2);
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
            }  // Retrieve group members
            group.MemberIDs = GetGroupMembersById(groupId);

            return group;
        }

        public List<int> GetGroupMembersById(int groupId)
        {
            List<int> memberIDs = new List<int>();

            using (SqlConnection con = DatabaseConnection.CreateConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT member_id FROM groupmember WHERE group_id = @group_id", con))
                    {
                        cmd.Parameters.AddWithValue("@group_id", groupId);
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                memberIDs.Add(reader.GetInt32(0));
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
            }

            return memberIDs;
        }
    }
}