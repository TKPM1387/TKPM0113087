using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHocSinh.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetReportByClass(string grade)
        {
            return View();
        }


        public ActionResult ReportByClass()
        {

            return View();
        }

	}
}