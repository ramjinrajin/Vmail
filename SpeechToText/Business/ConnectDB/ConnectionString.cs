using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SpeechToText.Business.ConnectDB
{

    public static class ConnectRemoteSql
    {
        public static string GetConnectionString
        {
            get
            {
                return "workstation id=SinglePageApp.mssql.somee.com;packet size=4096;user id=inzcloud_SQLLogin_1;pwd=8x5hm9hl6g;data source=SinglePageApp.mssql.somee.com;persist security info=False;initial catalog=SinglePageApp";

            }
        }
    }


    public class ExecuteSql
    {




        public static SqlConnection SqlConnect()
        {
            
          string connectionString = GetConnectionString();  
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        private static string GetConnectionString()
        {
            string DbLocation = Environment.CurrentDirectory.ToString();
            string ConnectionString = string.Format(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename={0}\YCET.mdf;Integrated Security=True", DbLocation);
            return ConnectionString;
            //string MasterConString = System.IO.File.ReadAllText(@"C:\\Ycet\\SqlDBConnect.ini");
            //return MasterConString.Replace("master", "YCET");
        }

        public static SqlCommand ExecuteCommand(string Query)
        {

            SqlCommand cmd = new SqlCommand(Query, ExecuteSql.SqlConnect());
            return cmd;
        }

        public static SqlCommand ExecuteProcedure(string Query)
        {

            SqlCommand cmd = new SqlCommand(Query, ExecuteSql.SqlConnect());
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

    }
}
