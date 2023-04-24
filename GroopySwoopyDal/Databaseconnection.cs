using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GroopySwoopyDAL
{
    internal static class DatabaseConnection
    {
        public static SqlConnection CreateConnection()
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());

            return connection;
        }

        private static string GetConnectionString()
        {
            //TODO: Should read this from a file
            string host = "localhost";
            string username = "rhc_admin";
            string password = "r00t";
            string database = "rockstars_health_check";

            //host = "lauren-healthcheck.db.transip.me";
            //username = "lauren_hcadmin";
            //password = "r00tpass";
            //database = "lauren_healthcheck";
            //string cs = $"Server={host};Uid={username};Pwd={password};Database={database}";


            //return "Server=studmysql01.fhict.local;Uid=dbi469729;Database=dbi469729;Pwd=test;";
            return "Data Source=fontysgroopyswoopy.database.windows.net;Initial Catalog=groopyswoopydatabase;User ID=beheerder;Password=Testtest!;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //return cs;
        }
    }
}
