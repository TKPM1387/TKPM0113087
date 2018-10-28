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

        public ActionResult getClassByLevel(string idLevel)
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
                       value ="0",
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
                        Address = dr[6].ToString()
                    };
                    students.Add(student);
                    i++;
                }
            }
            return Json(students, JsonRequestBehavior.AllowGet);
        }

    }
}