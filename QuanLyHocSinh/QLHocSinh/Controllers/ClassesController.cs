
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
    public class ClassesController : Controller
    {
        //
        // GET: /Classes/
        [CheckLogin]
        public ActionResult Index()
        {
            return View();
        }
        [CheckLogin]
        public ActionResult Search()
        {
            return View();
        }
        string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
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

        public ActionResult getClassLevel()
        {

            var result = new ArrayList();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("getClassLevel", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(
                    new
                    {
                        value = Convert.ToInt32(dr[0].ToString()),
                        text = dr[1].ToString()
                    });
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }
        public ActionResult getClassLevel2()
        {

            var result = new ArrayList();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("getClassLevel", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                var dr = cmd.ExecuteReader();
                result.Add(
                    new
                    {
                        value = "0",
                        text = "Tất cả"
                    });
                while (dr.Read())
                {
                    result.Add(
                    new
                    {
                        value = Convert.ToInt32(dr[0].ToString()),
                        text = dr[1].ToString()
                    });
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        public ActionResult getClassByLevel(int idLevel)
        {
            using (var ctx = new QLHSEntities())
            {
                if (idLevel == 0)
                {
                    var list = ctx.Classes
                       .Where(m => 1 == 1)
                       .OrderBy(m => m.ClassName)
                       .Select(m => new
                       {
                           value = m.ID,
                           text = m.ClassName
                       }).ToList();
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var list = ctx.Classes
                       .Where(m => m.ClassLevel == idLevel)
                       .OrderBy(m => m.ClassName)
                       .Select(m => new
                       {
                           value = m.ID,
                           text = m.ClassName
                       }).ToList();
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            //var result = new ArrayList();
            //var con = new SqlConnection(path);
            //var cmd = new SqlCommand("getClassByLevel", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@iDLevel", idLevel));
            //con.Open();
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //con.Close();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    result.Add(
            //       new
            //       {
            //           value = Convert.ToInt32(dr[0].ToString()),
            //           text = dr[1].ToString()
            //       });
            //}
            //return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getClassByLevel2(string idLevel)
        {
            var result = new ArrayList();
            var con = new SqlConnection(path);
            var cmd = new SqlCommand("getClassByLevel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@iDLevel", idLevel));
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            result.Add(
                   new
                   {
                       value = "0",
                       text = "Tất cả"
                   });
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(
                   new
                   {
                       value = Convert.ToInt32(dr[0].ToString()),
                       text = dr[1].ToString()
                   });
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetStudentDetail(string idname)
        {
            using (var ctx = new QLHSEntities())
            {
                var pu = ctx.Students
                        .Join(ctx.Classes
                        , u => u.Class, uir => uir.ID, (u, uir) => new { u, uir })
                        .Where(
                            m => m.u.State != -1 &&
                            (m.u.StudentID.Contains(idname) || m.u.FullName.Contains(idname))
                            )
                        .OrderBy(m => m.u.Class)
                        .Select(m => new
                        {
                            StudentID = m.u.StudentID,
                            FullName = m.u.FullName,
                            ClassName = m.uir.ClassName,
                            TBHK1 = 4,
                            TBHK2 = 5
                        }).ToList();
                return Json(pu, JsonRequestBehavior.AllowGet);
            }

            //string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            //int i = 1;
            //var students = new List<StudentDetail>();
            //using (var con = new SqlConnection(path))
            //{
            //    var cmd = new SqlCommand("getStudentDetail", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add(new SqlParameter("@idname", idname));
            //    con.Open();
            //    var dr = cmd.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        var student = new StudentDetail
            //        {
            //            STT = i,
            //            StudentID = Convert.ToInt32(dr[0].ToString()),
            //            FullName = dr[1].ToString(),
            //            Class = dr[2].ToString(),
            //            TBHK1 = Convert.ToInt32(dr[3].ToString()),
            //            TBHK2 = Convert.ToInt32(dr[4].ToString()),
            //        };
            //        students.Add(student);
            //        i++;
            //    }
            //}
            //return Json(students, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentPoint(int grade, string subject)
        {
            var result = new ArrayList();
            using (var ctx = new QLHSEntities())
            {
                var list = (from s in ctx.Students
                            from p in ctx.Points.Where(p1 => p1.StudentID == s.StudentID && p1.SubjectID == subject).DefaultIfEmpty()
                            where s.Class == grade
                            select new
                            {
                                StudentID = s.StudentID,
                                FullName = s.FullName,
                                Average = p.Average,
                                Test15Minutes = p.Test15Minutes,
                                Test45Minutes = p.Test45Minutes,
                                TestSemester = p.TestSemester
                            }).ToList();


                //var list = ctx.Students
                //      .Join(ctx.Points
                //      , u => u.StudentID, uir => uir.StudenID, (u, uir) => new { u, uir })
                //      .Where(m => m.u.Class == grade && m.uir.SubjectID == subject && m.uir.Flag != -1)
                //      .OrderBy(m => m.u.StudentID)
                //      .Select(m => new
                //      {
                //          StudentID = m.u.StudentID,
                //          FullName = m.u.FullName,
                //          Test15Minutes = m.uir.Test15Minutes,
                //          Test45Minutes = m.uir.Test45Minutes,
                //          TestSemester = m.uir.TestSemester,
                //          Average = m.uir.Average
                //      }).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }

            //string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            //int i = 1;
            //var students = new List<StudentPoint>();
            //using (var con = new SqlConnection(path))
            //{
            //    var cmd = new SqlCommand("getStudentPoint", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add(new SqlParameter("@fsubjectid", grade));
            //    cmd.Parameters.Add(new SqlParameter("@fclassid", subject));
            //    con.Open();
            //    var dr = cmd.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        var student = new StudentPoint
            //        {
            //            STT = i,
            //            StudentID = Convert.ToInt32(dr[0].ToString()),
            //            FullName = dr[1].ToString(),
            //            Test15Minutes = dr[2].ToString(),
            //            Test45Minutes = dr[3].ToString(),
            //            TestSemester = dr[4].ToString(),
            //        };
            //        students.Add(student);
            //        i++;
            //    }
            //}
            //return Json(students, JsonRequestBehavior.AllowGet);
        }

        [CheckLogin]
        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetListClass()
        {
            using (var ctx = new QLHSEntities())
            {
                var pu = ctx.Classes
                    .Join(ctx.ClassLevels
                    , u => u.ClassLevel, uir => uir.ID, (u, uir) => new { u, uir })
                    .Where(m => 1 == 1)
                    .OrderBy(m => m.u.ClassLevel)
                    .Select(m => new
                    {
                        ID = m.u.ID,
                        ClassName = m.u.ClassName,
                        Total = m.u.Total,
                        MaxTotal = m.u.MaxTotal,
                        Level = m.uir.LevelName
                    }).ToList();

                //var pu = ctx.Classes.GroupJoin(ctx.ClassLevels,
                //                c => c.ClassLevel,
                //                le => l.ID,
                //                (c, le) => new
                //                {
                //                    ID = c.ID,
                //                    ClassName = c.ClassName,
                //                    LevelName=

                //                }).ToList();

                return Json(pu, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListAllClass()
        {
            using (var ctx = new QLHSEntities())
            {
                var pu = ctx.Classes
                    .Join(ctx.ClassLevels
                    , u => u.ClassLevel, uir => uir.ID, (u, uir) => new { u, uir })
                    .Where(m => 1 == 1)
                    .OrderBy(m => m.u.ClassLevel)
                    .Select(m => new
                    {
                        ClassID = m.u.ID,
                        ClassName = m.u.ClassName,
                        ClassLevelName = m.uir.LevelName,
                        Total = m.u.Total,
                        MaxTotal = m.u.MaxTotal
                    }).ToList();


                return Json(pu, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult UpdateClass(Class c)
        {
            var result = new ArrayList();
            using (var ctx = new QLHSEntities())
            {
                var classx = ctx.Classes.Where(cl => cl.ID == c.ID).FirstOrDefault();

                classx.ClassName = c.ClassName;
                classx.MaxTotal = c.MaxTotal;

                ctx.SaveChanges();
                result.Add(
                    new
                    {
                        value = 1
                    });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public bool CheckMaxClass(int classlevel)
        {
            using (var ctx = new QLHSEntities())
            {
                //var level = ctx.Classes.Where(c => c.ID == classid).FirstOrDefault().ClassLevel;

                var currenttotal = ctx.Classes.Where(c => c.ClassLevel == classlevel).ToList();
                var maxtotal = ctx.ClassLevels.Where(l => l.ID == classlevel).FirstOrDefault().MaxTotal;

                if (currenttotal.Count >= maxtotal)
                {
                    return false;
                }
                return true;
            }
        }



        [HttpPost]
        public ActionResult AddClass(Class c)
        {
            int t = int.Parse(c.ClassLevel.ToString());
            var result = new ArrayList();
            if (!CheckMaxClass(t))
            {
                result.Add(
                    new
                    {
                        value = -1,
                        message="Khối đã đạt số lượng tối đa"
                    });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            c.MaxTotal = 40;
            c.Total = 0;
            c.Flag = 1;
            using (var ctx = new QLHSEntities())
            {

                ctx.Classes.Add(c);

                ctx.SaveChanges();
                result.Add(
                    new
                    {
                        value = 1
                    });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getTotalInClass(int ID)
        {
            var result = new ArrayList();
            using (var ctx = new QLHSEntities())
            {
                var cl = ctx.Classes.Where(c => c.ID == ID).FirstOrDefault();
                return Json(cl, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetStudentPointDetail(string ID, int semester)
        {
            var result = new ArrayList();
            using (var ctx = new QLHSEntities())
            {
                var name = (from s in ctx.Students where s.StudentID == ID select s.FullName).FirstOrDefault().ToString();

                var list = (from sj in ctx.Subjects
                            from p in ctx.Points.Where(p1 => p1.SubjectID == sj.SubjectID && p1.Semester == semester).DefaultIfEmpty()
                            from st in ctx.Students.Where(s1 => s1.StudentID == p.StudentID && p.Semester == semester && s1.StudentID == ID).DefaultIfEmpty()
                            //where st.StudentID == ID
                            select new
                            {
                                StudentID = ID,
                                FullName = name,
                                SubjectName=sj.SubjectName,
                                Average = p.Average,
                                Test15Minutes = p.Test15Minutes,
                                Test45Minutes = p.Test45Minutes,
                                TestSemester = p.TestSemester

                            }).ToList();

                /* SELECT  S.ID ,
                 S.StudentID ,
                 S.FullName ,
                 P.Semester ,
                 P.Test15Minutes ,
                 P.Test45Minutes ,
                 P.TestSemester,S2.SubjectID,S2.SubjectName
                 FROM    dbo.Subject AS S2
                 LEFT JOIN dbo.Point AS P ON S2.SubjectID = P.SubjectID
                 LEFT JOIN ( SELECT  *
                             FROM    dbo.Students
                             WHERE   StudentID = 'HS00007'
                           ) AS S ON S.StudentID = P.StudenID
                                     AND P.Semester = 1*/



                //var list = ctx.Points
                //     .Join(ctx.Students
                //     , poi => poi.StudenID, stu => stu.StudentID, (poi, stu) => new { poi, stu })
                //     .Join(ctx.Subjects
                //     , p => p.poi.SubjectID, s => s.SubjectID, (p, s) => new { p.stu, p.poi, s }
                //     )
                //     .Where(m => m.stu.StudentID == ID && m.poi.Semester == semester)
                //     .Select(m => new
                //     {
                //         StudentID = m.stu.StudentID,
                //         FullName = m.stu.FullName,
                //         SubjectName = m.s.SubjectName,
                //         Test15Minutes = m.poi.Test15Minutes,
                //         Test45Minutes = m.poi.Test45Minutes,
                //         TestSemester = m.poi.TestSemester,
                //         Average = m.poi.Average,
                //     }).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }


    }
}