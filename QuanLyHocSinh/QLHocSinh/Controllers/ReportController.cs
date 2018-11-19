using QLHocSinh.Helper;
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

        [CheckLogin]
        public ActionResult Index()
        {
            return View();
        }

        [CheckLogin]
        public ActionResult ReportBySubject()
        {
            return View();
        }
        [CheckLogin]
        public ActionResult GetReportByClass(string grade)
        {
            return View();
        }


        [CheckLogin]
        public ActionResult ReportByClass()
        {

            return View();
        }

	}
}