using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Test.Libs.CRUD
{
    public interface ISqlCommand
    {
        string CommandText { get; set; }
        string SPName { get; set; }
        List<SqlParameter> SqlParams { get; set; }
        bool Run();
        DataSet SelectData();
        string JSONData();
    }
}
