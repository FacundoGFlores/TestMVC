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
            cmd.CommandText = "SELECT StudentID, FirstName, Age FROM Students";
            var data = objStudent.Select(cmd);

            foreach (DataRow item in data.Tables[0].Rows)
            {
                model.Add(new Student
                {
                    StudentID = Convert.ToInt32(item["StudentID"]),
                    FirstName = item["FirstName"].ToString(),
                    Age = item.IsNull("Age") ? null : (int?) Convert.ToInt32(item["Age"])
                });
            }
    
            return View("Index", model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            var objStudent = new Student();
            var cmd = new SqlBuilder();
            cmd.CommandText = "INSERT INTO Students (FirstName, Age) VALUES (@FirstName, @Age)";
            cmd.SqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@FirstName", student.FirstName),
                new SqlParameter("@Age", student.Age),
            };
            var isInserted = objStudent.Insert(cmd);
            return RedirectToAction("Index");
        }

    }
}
