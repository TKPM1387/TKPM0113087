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
    public class ClassesController : Controller
    {
        //
        // GET: /Classes/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getClassLevel()
        {
            {
                string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";

                var clevel = new List<ClassLevel>();
                using (var con = new SqlConnection(path))
                {
                    var cmd = new SqlCommand("getClassLevel", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var cllevel = new ClassLevel
                        {
                            iD = Convert.ToInt32(dr[0].ToString()),
                            levelName = dr[1].ToString(),
                            maxTotal = Convert.ToInt32(dr[2].ToString()),
                        };
                        clevel.Add(cllevel);
                    }
                }
                return Json(clevel, JsonRequestBehavior.AllowGet);
            }

        }
    }
}