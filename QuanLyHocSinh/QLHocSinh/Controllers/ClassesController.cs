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
    public class ClassesController : Controller
    {
        //
        // GET: /Classes/
        public ActionResult Index()
        {
            return View();
        }
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
        public ActionResult getNodeClass()
        {
            {

                var listNode = new List<NodeClass>();
                var con = new SqlConnection(path);
                var cmd = new SqlCommand("getClassLevel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    //var conc = new SqlConnection(path);
                    string idlv = dr[0].ToString();
                    var clnodes = new List<NodeClass>();
                    var cmdc = new SqlCommand("getClassByLevel", con);
                    cmdc.CommandType = CommandType.StoredProcedure;
                    cmdc.Parameters.Add(new SqlParameter("@iDLevel", idlv));
                    con.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter();
                    da1.SelectCommand = cmdc;
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    con.Close();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        var cnode = new NodeClass
                        {
                            iD = Convert.ToInt32(dr1[0].ToString()),
                            //text = "<a onclick=\"myFunction()\">Try it</a>",//dr1[1].ToString(),
                            text = "<a onclick=\"myFunction()\">" + dr1[1].ToString() + "</a>",
                            href = "https://www.google.com.vn/",
                            tags = "['4']",
                        };
                        clnodes.Add(cnode);
                    }
                    var nodes = new NodeClass
                    {
                        iD = Convert.ToInt32(dr[0].ToString()),
                        text = dr[1].ToString(),
                        href = "https://www.google.com.vn/",
                        tags = "['4']",
                        nodes = clnodes,
                    };
                    listNode.Add(nodes);

                }

                return Json(listNode, JsonRequestBehavior.AllowGet);
            }

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

        public ActionResult GetClasses()
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            int i = 1;
            var classes = new List<Classes>();
            using (var con = new SqlConnection(path))
            {
                var cmd = new SqlCommand("GetClasses", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var cl = new Classes
                    {
                        STT = i,
                        ClassID = Convert.ToInt32(dr[0].ToString()),
                        ClassName = dr[1].ToString(),
                        ClassLevelName = dr[2].ToString()
                    };
                    classes.Add(cl);
                    i++;
                }
            }
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetStudents()
        //{
        //    string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
        //    int i = 1;
        //    var students = new List<Students>();
        //    using (var con = new SqlConnection(path))
        //    {
        //        var cmd = new SqlCommand("getStudents", con) { CommandType = CommandType.StoredProcedure };
        //        con.Open();
        //        var dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            var student = new Students
        //            {
        //                STT = i,
        //                StudentID =dr[0].ToString(),
        //                FullName = dr[1].ToString(),
        //                BirthDay = Convert.ToDateTime(dr[2].ToString()),
        //                Gender = Convert.ToInt32(dr[3].ToString()),
        //                Address = dr[6].ToString()
        //            };
        //            students.Add(student);
        //            i++;
        //        }
        //    }
        //    return Json(students, JsonRequestBehavior.AllowGet);
        //}

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
                var list = ctx.Students
                      .Join(ctx.Points
                      , u => u.StudentID, uir => uir.StudenID, (u, uir) => new { u, uir })
                      .Where(m => m.u.Class == grade && m.uir.SubjectID == subject)
                      .OrderBy(m => m.u.StudentID)
                      .Select(m => new
                      {
                          StudentID = m.u.StudentID,
                          FullName = m.u.FullName,
                          Test15Minutes = m.uir.Test15Minutes,
                          Test45Minutes = m.uir.Test45Minutes,
                          TestSemester = m.uir.TestSemester,
                          Average = m.uir.Average
                      }).ToList();

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

        [HttpPost]
        public ActionResult AddClass(Class c)
        {
            var result = new ArrayList();
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
                var list = ctx.Points
                     .Join(ctx.Students
                     , poi => poi.StudenID, stu => stu.StudentID, ( poi,stu) => new {  poi,stu })
                     .Join(ctx.Subjects
                     , p => p.poi.SubjectID, s => s.SubjectID, (p, s) => new { p.stu, p.poi, s }
                     )
                     .Where(m => m.stu.StudentID == ID && m.poi.Semester == semester)                     
                     .Select(m => new
                     {
                         StudentID = m.stu.StudentID,
                         FullName=m.stu.FullName,
                         SubjectName=m.s.SubjectName,
                         Test15Minutes=m.poi.Test15Minutes,
                         Test45Minutes=m.poi.Test45Minutes,
                         TestSemester=m.poi.TestSemester,
                         Average = m.poi.Average,
                     }).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        /*
          SELECT  s.StudentID ,
        s.FullName ,
        S2.SubjectName ,
        P.Test15Minutes ,
        P.Test45Minutes ,
        P.TestSemester ,
        P.Average
FROM    dbo.Students AS s
        JOIN dbo.Point AS P ON s.StudentID = P.StudenID
        JOIN dbo.Subject AS S2 ON P.SubjectID = S2.SubjectID
*/
    }
}