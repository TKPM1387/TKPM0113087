using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHocSinh.Models
{
    public class StudentDetail
    {
        public int STT { get; set; }
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Class { get; set; }
        public int TBHK1 { get; set; }
        public int TBHK2 { get; set; }
    }
}