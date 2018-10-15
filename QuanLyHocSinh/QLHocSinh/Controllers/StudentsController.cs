using QLHocSinh.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHocSinh.Controllers
{
    public class StudentsController : Controller
    {
        string path = @"Data Source=.\SQLSERVER;Initial Catalog=QLHS;Integrated Security=True";
        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            List<Dictionary<string, object>>
            lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in dtData.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in dtData.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        }
        //
        // GET: /Students/

        SqlDataAdapter _globalAdapt;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetStudents()
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

            var students = new List<Students>();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("getStudents", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var student = new Students
                    {
                        iD = Convert.ToInt32(dr[0].ToString()),
                        firstName = dr[1].ToString(),
                        lastName = dr[2].ToString(),
                        feesPaid = Convert.ToInt32(dr[3].ToString()),
                        gender = dr[4].ToString(),
                        emailId = dr[5].ToString(),
                        telephoneNumber = dr[6].ToString(),
                        dateOfBirth = Convert.ToDateTime(dr[7].ToString())
                    };
                    students.Add(student);
                }
            }
            //var js = new JavaScriptSerializer();
            //Context.Response.Write(js.Serialize(students));  
            //return View();
            return Json(students, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNewStudent(string fname, string lname)
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";
            var con = new SqlConnection(path);
            var cmd = new SqlCommand("addStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@fname", fname));
            cmd.Parameters.Add(new SqlParameter("@lname", lname));
            con.Open();
            int n = cmd.ExecuteNonQuery();
            con.Close();

            return Json(n, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getListStudents()
        {
            SqlConnection cnn = new SqlConnection(path);

            string sql = "Select * from Students";
            _globalAdapt = new SqlDataAdapter(sql, cnn);

            DataTable table = new DataTable();
            _globalAdapt.Fill(table);
            List<Dictionary<string, object>> ds;

            ds = GetTableRows(table);
            var json = Json(ds, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }
        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent1(string s)
        {
            return View();
        }

        [HttpPost]
        public ActionResult testadd(Students stu)
        {
            int a = 1;

         var b = new ArrayList()
        {
            new { Value = 4, Display = "Emily" },
            new { Value = 5, Display = "Lauri" },
        };
            return Json(b, JsonRequestBehavior.AllowGet);  
        }
    }
}