using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Test.Libs.CRUD
{
    public class SqlBuilder: ISqlCommand
    {
        private ISqlConnection connection = null;
        private SqlConnection sqlConnection = null;

        public SqlBuilder()
        {
            connection = new ConnectionManager();
            sqlConnection = connection.Connection;
        }

        public string CommandText { get; set; }
        public string SPName { get; set; }

        public List<SqlParameter> SqlParams { get; set; }
        
        public bool Run()
        {
            bool IsExecuted = false;
            using(sqlConnection)
            {
                try{
                    sqlConnection.Open();
                    using(var sqlCommand = new SqlCommand(CommandText, sqlConnection))
                    {
                        sqlCommand.CommandType = !string.IsNullOrWhiteSpace(CommandText) ? 
                            CommandType.Text : CommandType.StoredProcedure;
                        sqlCommand.CommandText = !string.IsNullOrEmpty(CommandText) ?
                            CommandText : SPName;
                        if(SqlParams != null)
                        {
                            SqlParams.ForEach(p => sqlCommand.Parameters.Add(p));
                        }
                        IsExecuted = sqlCommand.ExecuteNonQuery() > 0;
                    }
                }
                catch(Exception)
                {
                    System.Console.WriteLine("Failed");
                }
            }
            return IsExecuted;
        }

        public DataSet SelectData()
        {
            var data = new DataSet();
            using(sqlConnection)
            {
                try{
                    sqlConnection.Open();
                    using(var sqlCommand = new SqlCommand(CommandText, sqlConnection))
                    {
                        sqlCommand.CommandType = !string.IsNullOrWhiteSpace(CommandText) ? 
                            CommandType.Text : CommandType.StoredProcedure;
                        sqlCommand.CommandText = !string.IsNullOrEmpty(CommandText) ?
                            CommandText : SPName;
                        if (SqlParams != null)
                        {
                            SqlParams.ForEach(p => sqlCommand.Parameters.Add(p));
                        };
                        var adapter = new SqlDataAdapter(sqlCommand);
                        adapter.Fill(data);
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Failed");
                }
            }
            return data;
        }
    }
}