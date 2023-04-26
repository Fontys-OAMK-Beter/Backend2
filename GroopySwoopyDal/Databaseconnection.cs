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
           
            return "Data Source=fontysgroopyswoopy.database.windows.net;Initial Catalog=groopyswoopydatabase;User ID=beheerder;Password=Testtest!;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        
        }
    }
}
