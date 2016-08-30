using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Libs.CRUD
{
    interface ICrud<T>
    {
        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(T obj);
        DataSet Select(T obj);
        string JSONSelect(T obj);
    }
}
