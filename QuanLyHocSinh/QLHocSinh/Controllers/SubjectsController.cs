using QLHocSinh.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHocSinh.Controllers
{
    public class SubjectsController : Controller
    {
        //
        // GET: /Subjects/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getSubjects()
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            int i = 1;
            var sjs = new List<Subjects>();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("getSubjects", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var sj = new Subjects
                    {
                        STT = i,
                        SubjectID = Convert.ToInt32(dr[0].ToString()),
                        SubjectName = dr[1].ToString(),
                        Level = dr[2].ToString()
                    };
                    sjs.Add(sj);
                    i++;
                }
            }
            return Json(sjs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getListSubject()
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            int i = 1;
            var sjs = new List<Subjects>();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("getListSubject", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var sj = new Subjects
                    {
                        SubjectID = Convert.ToInt32(dr[0].ToString()),
                        SubjectName = dr[1].ToString(),
                    };
                    sjs.Add(sj);
                    i++;
                }
            }
            return Json(sjs, JsonRequestBehavior.AllowGet);
        }
	}
}