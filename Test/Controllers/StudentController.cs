using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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
            string connectionString = GetConnectionString();
            var model = new List<Student>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT FirstName FROM Students", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        model.Add(new Student {FirstName = reader["FirstName"].ToString()});
                    }
                }
                con.Close();
            }


            return View("Index", model);
        }

        public ActionResult SaveStudent()
        {
            string connectionString = GetConnectionString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "INSERT INTO Students (FirstName) VALUES (@FirstName)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@FirstName", Request.Form["name"].ToString());
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        public ActionResult FillStudent()
        {
            return View();
        }
    }
}
