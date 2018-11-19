
using QLHocSinh.Helper;
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
    public class SubjectsController : Controller
    {
        //
        // GET: /Subjects/
        [CheckLogin]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getSubjects()
        {
            using (var ctx = new QLHSEntities())
            {
                var list = ctx.Subjects.Where(p => 1 == 1).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
            //string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            //int i = 1;
            //var sjs = new List<Subjects>();
            //using (var con = new SqlConnection(path))
            //{
            //    var cmd = new SqlCommand("getSubjects", con) { CommandType = CommandType.StoredProcedure };
            //    con.Open();
            //    var dr = cmd.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        var sj = new Subjects
            //        {
            //            STT = i,
            //            SubjectID = Convert.ToInt32(dr[0].ToString()),
            //            SubjectName = dr[1].ToString(),
            //            Level = dr[2].ToString(),
            //            Period= int.Parse(dr[3].ToString())
            //        };
            //        sjs.Add(sj);
            //        i++;
            //    }
            //}
            //return Json(sjs, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getListSubject()
        {
            using (var ctx = new QLHSEntities())
            {
                var list = ctx.Subjects.Where(p => p.Flag != -1).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            //string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            //int i = 1;
            //var sjs = new List<Subjects>();
            //using (var con = new SqlConnection(path))
            //{
            //    var cmd = new SqlCommand("getListSubject", con) { CommandType = CommandType.StoredProcedure };
            //    con.Open();
            //    var dr = cmd.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        var sj = new Subjects
            //        {
            //            SubjectID = Convert.ToInt32(dr[0].ToString()),
            //            SubjectName = dr[1].ToString(),
            //        };
            //        sjs.Add(sj);
            //        i++;
            //    }
            //}
            //return Json(sjs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateSubject(Subject s)
        {
            var result = new ArrayList();
            using (var ctx = new QLHSEntities())
            {
                var subject = ctx.Subjects.Where(sub => sub.SubjectID == s.SubjectID).FirstOrDefault();

                subject.SubjectName = s.SubjectName;
                subject.Period = s.Period;
                subject.Type = s.Type;

                ctx.SaveChanges();
                result.Add(
                    new
                    {
                        value = 1
                    });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult AddSubject(Subject s)
        {
            var result = new ArrayList();
            using (var ctx = new QLHSEntities())
            {
                ctx.Subjects.Add(s);

                ctx.SaveChanges();
                result.Add(
                    new
                    {
                        value = 1
                    });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteSubject(string id)
        {
            var result = new ArrayList();
            using (var ctx = new QLHSEntities())
            {
                var s = ctx.Subjects.Where(ss => ss.SubjectID == id).FirstOrDefault();
                s.Flag = -1;
                ctx.SaveChanges();
                result.Add(
                    new
                    {
                        value = 1
                    });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}