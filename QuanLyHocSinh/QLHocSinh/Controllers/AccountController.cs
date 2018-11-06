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
            if (CurrentContext.IsLogged() == true)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Login = 1;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            //string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLHS;Integrated Security=True";
            //var con = new SqlConnection(path);
            //var cmd = new SqlCommand("checklogin", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@fusername", username));
            //cmd.Parameters.Add(new SqlParameter("@fpassword", password));
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //con.Close();
            using (QLHSEntities dt = new QLHSEntities())
            {
                User us = dt.Users
                        .Where(p => p.username == username && p.password == password)
                        .FirstOrDefault();

                if (us != null)
                {
                    Session["isLogin"] = 1;
                    Session["user"] = us;
                    Session["IdUser"] = us.id;
                    Session["username"] = us.username;
                    Session["role"] = us.Role;
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Login = 2;
            return View();
        }

        public ActionResult Logout()
        {
            CurrentContext.Destroy();
            return RedirectToAction("Index", "Home");
        }
	}
}