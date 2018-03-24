using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace VmailApiAuth.Infrastructure
{
    public static class ConnectSql
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["RemoteVmail"].ConnectionString.ToString();
                 
            }
        }
    }
}