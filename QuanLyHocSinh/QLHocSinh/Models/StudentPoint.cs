using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHocSinh.Models
{
    public class StudentPoint
    {
        public int STT { get; set; }
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Test15Minutes { get; set; }
        public string Test45Minutes { get; set; }
        public string TestSemester { get; set; }
       
        
    }
}