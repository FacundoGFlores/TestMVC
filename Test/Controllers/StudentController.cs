using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Test.Libs.CRUD;

namespace Test.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        static private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["TestMVCConnectionString"].ConnectionString;
        }
        public ActionResult Index()
        {
            var model = new List<Student>();
           
            var objStudent = new Student();
            var cmd = new SqlBuilder();
            cmd.CommandText = "SELECT FirstName FROM Students";
            var data = objStudent.Select(cmd);

            foreach (DataRow item in data.Tables[0].Rows)
            {
                model.Add(new Student
                {
                    FirstName = item["FirstName"].ToString()
                });
            }
    
            return View("Index", model);
        }



        public ActionResult FillStudent()
        {
            return View();
        }
    }
}
