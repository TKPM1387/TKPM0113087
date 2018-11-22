using Newtonsoft.Json;
using QLHocSinh.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHocSinh.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        string path = @"Data Source=.\SQLSERVER;Initial Catalog=QLHS;Integrated Security=True";
        [CheckLogin]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReportBySubject()
        {
            return View();
        }

        public ActionResult ReportBySemester()
        {
            return View();
        }
        public ActionResult GetReportBySubject(int semester, string subjectid)
        {
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("ReportFolowSubject", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Year", 0));
                cmd.Parameters.Add(new SqlParameter("@Semester", semester));
                cmd.Parameters.Add(new SqlParameter("@SubjectID", subjectid));
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                var tb = new DataTable();
                sda.Fill(tb);
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(tb);
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetReportBySemester(int semester)
        {
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("ReportFolowYear", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Year", 0));
                cmd.Parameters.Add(new SqlParameter("@Semester", semester));
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                var tb = new DataTable();
                sda.Fill(tb);
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(tb);
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }
        }
    }
}