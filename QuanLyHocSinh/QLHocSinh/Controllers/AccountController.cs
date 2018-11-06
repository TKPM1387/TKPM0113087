using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHocSinh.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Login = 1;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            var con = new SqlConnection(path);
            var cmd = new SqlCommand("checklogin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@fusername", username));
            cmd.Parameters.Add(new SqlParameter("@fpassword", password));
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewBag.Login = 2;
            return View();
        }
	}
}