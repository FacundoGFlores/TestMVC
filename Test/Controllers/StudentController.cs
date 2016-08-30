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

        public string GetAllStudents()
        {
            var objStudent = new Student();
            var cmd = new SqlBuilder();
            cmd.CommandText = "SELECT StudentID, FirstName, Age FROM Students";
            return objStudent.JSONSelect(cmd);
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


        public ActionResult Edit(int id = 0)
        {
            var objStudent = new Student();
            var cmd = new SqlBuilder();
            cmd.CommandText = "SELECT StudentID, FirstName, Age FROM Students WHERE StudentID = @where";
            cmd.SqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@where", id),
            };
            var data = objStudent.Select(cmd);
            if (data == null)
            {
                return HttpNotFound();
            }
            else
            {
                DataRow row = data.Tables[0].Rows[0];
                Student s = new Student { StudentID = Convert.ToInt32(row["StudentID"]), FirstName = row["FirstName"].ToString(), Age = row.IsNull("Age") ? null : (int?)Convert.ToInt32(row["Age"]) };
                return View(s);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            var objStudent = new Student();
            objStudent.StudentID = student.StudentID;
            objStudent.FirstName = student.FirstName;
            objStudent.Age = student.Age;
            var cmd = new SqlBuilder();
            cmd.CommandText = "UPDATE Students SET FirstName = @FirstName, Age = @Age WHERE StudentID = @where";
            cmd.SqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@FirstName", objStudent.FirstName),
                new SqlParameter("@Age", objStudent.Age),
                new SqlParameter("@where", objStudent.StudentID)
            };
            var isInserted = objStudent.Update(cmd);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id = 0)
        {
            var objStudent = new Student();
            var cmd = new SqlBuilder();
            cmd.CommandText = "SELECT StudentID, FirstName, Age FROM Students WHERE StudentID = @where";
            cmd.SqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@where", id),
            };
            var data = objStudent.Select(cmd);
            if (data == null)
            {
                return HttpNotFound();
            }
            else
            {
                DataRow row = data.Tables[0].Rows[0];
                Student s = new Student { StudentID = Convert.ToInt32(row["StudentID"]), FirstName = row["FirstName"].ToString(), Age = row.IsNull("Age") ? null : (int?)Convert.ToInt32(row["Age"]) };
                return View(s);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var objStudent = new Student();
            var cmd = new SqlBuilder();
            cmd.CommandText = "DELETE Students WHERE StudentID = @where";
            cmd.SqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@where", id)
            };
            var isInserted = objStudent.Delete(cmd);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            var objStudent = new Student();
            var cmd = new SqlBuilder();
            cmd.CommandText = "SELECT StudentID, FirstName, Age FROM Students WHERE StudentID = @where";
            cmd.SqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@where", id),
            };
            var data = objStudent.Select(cmd);
            if (data == null)
            {
                return HttpNotFound();
            }
            else
            {
                DataRow row = data.Tables[0].Rows[0];
                Student s = new Student { StudentID = Convert.ToInt32(row["StudentID"]), FirstName = row["FirstName"].ToString(), Age = row.IsNull("Age") ? null : (int?)Convert.ToInt32(row["Age"]) };
                return View(s);
            }
        }
    }

}
