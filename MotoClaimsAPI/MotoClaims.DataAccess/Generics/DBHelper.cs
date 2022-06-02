using System;
using System.Configuration;

namespace MotoClaims.DataAccess.Generics
{
    public class DBHelper
    {
        public static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["MotoClaims"].ToString(); }
        }
    }
}