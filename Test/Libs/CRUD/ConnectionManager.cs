using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Test.Libs.CRUD
{
    public class ConnectionManager: ISqlConnection
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["TestMVCConnectionString"].ConnectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public SqlConnection Connection
        {
            get
            {
                return GetConnection();
            }
        }
    }
}