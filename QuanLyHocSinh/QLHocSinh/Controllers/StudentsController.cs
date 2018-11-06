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
        public ActionResult AddStudent()
        {
            return View();
        }
        public ActionResult Score()
        {
            return View();
        }
        public ActionResult UpdateScore()
        {
            return View();
        }
        public ActionResult GetStudents()
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            int i = 1;
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
                        STT = i,
                        StudentID = Convert.ToInt32(dr[0].ToString()),
                        FullName = dr[1].ToString(),
                        BirthDay = Convert.ToDateTime(dr[2].ToString()),
                        Gender = Convert.ToInt32(dr[3].ToString()),
                        Email = dr[4].ToString(),
                        PhoneNumber = dr[5].ToString(),
                        Address = dr[6].ToString()
                    };
                    students.Add(student);
                    i++;
                }
            }
            return Json(students, JsonRequestBehavior.AllowGet);
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
        public ActionResult getIDStudent()
        {
            int id = 1800000;
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            var con = new SqlConnection(path);
            var cmd = new SqlCommand("SELECT MAX(id) AS idm FROM Students", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            //if (dt.Rows.Count > 0)
            id += (dt.Rows[0][0].ToString() != "") ? int.Parse(dt.Rows[0][0].ToString()) : 0;
            //id += int.Parse(dt.Rows[0][0].ToString());

            var x = new ArrayList()
            {
                new { Value =id}
            };

            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getStudentByID(string id)
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
           
            var students = new List<Students>();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("getStudentByID", con);
                cmd.CommandType = CommandType.StoredProcedure ;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var student = new Students
                    {
                        StudentID = Convert.ToInt32(dr[1].ToString()),
                        FullName = dr[2].ToString(),
                        BirthDay = Convert.ToDateTime(dr[3].ToString()),
                        Gender = Convert.ToInt32(dr[4].ToString()),
                        Email = dr[5].ToString(),
                        PhoneNumber = dr[6].ToString(),
                        Address = dr[7].ToString(),
                        ClassLevel = Convert.ToInt32(dr[8].ToString()),
                        Class = Convert.ToInt32(dr[9].ToString())
                    };
                    students.Add(student);
                }
            }
            return Json(students, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddNewStudent(Students s)
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            var con = new SqlConnection(path);
            var cmd = new SqlCommand("addStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@fidstudent", s.StudentID));
            cmd.Parameters.Add(new SqlParameter("@ffullname", s.FullName));
            cmd.Parameters.Add(new SqlParameter("@fbirthday", s.BirthDay));
            cmd.Parameters.Add(new SqlParameter("@fgender", s.Gender));
            cmd.Parameters.Add(new SqlParameter("@femail", s.Email));
            cmd.Parameters.Add(new SqlParameter("@fphonenumber", s.PhoneNumber));
            cmd.Parameters.Add(new SqlParameter("@faddress", s.Address));
            con.Open();

            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //con.Close();

            int n = cmd.ExecuteNonQuery();
            var result = new ArrayList();
            result.Add(
                new
                {
                    value = n
                });


            con.Close();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentsByClass(string grade)
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            int i = 1;
            var students = new List<Students>();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("getStudentsByClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fclass", grade));
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var student = new Students
                    {
                        STT = i,
                        StudentID = Convert.ToInt32(dr[1].ToString()),
                        FullName = dr[2].ToString(),
                        BirthDay = Convert.ToDateTime(dr[3].ToString()),
                        Gender = Convert.ToInt32(dr[4].ToString()),
                        Email = dr[5].ToString(),
                        PhoneNumber = dr[6].ToString(),
                        Address = dr[7].ToString(),
                    };
                    students.Add(student);
                    i++;
                }
            }
            return Json(students, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateStudentPoint(StudentPoint s)
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            int i = 1;
            var students = new List<Students>();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("UpdateStudentPoint", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fids", s.StudentID));
                cmd.Parameters.Add(new SqlParameter("@fp15", s.Test15Minutes));
                cmd.Parameters.Add(new SqlParameter("@fp45", s.Test45Minutes));
                cmd.Parameters.Add(new SqlParameter("@fpl", s.TestSemester));
                con.Open();
                int n = cmd.ExecuteNonQuery();
                var result = new ArrayList();
                result.Add(
                    new
                    {
                        value = n
                    });


                con.Close();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
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