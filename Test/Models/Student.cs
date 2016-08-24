using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Test.Libs.CRUD;

namespace Test.Models
{
    public class Student: ICrud<ISqlCommand>
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public int? Age { get; set; }
        public bool Insert(ISqlCommand obj)
        {
            return obj.Run();
        }

        public bool Update(ISqlCommand obj)
        {
            return obj.Run();
        }

        public bool Delete(ISqlCommand obj)
        {
            return obj.Run();
        }

        public DataSet Select(ISqlCommand obj)
        {
            return obj.SelectData();
        }
    }
}