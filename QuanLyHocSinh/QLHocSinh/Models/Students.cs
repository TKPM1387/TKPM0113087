using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHocSinh.Models
{
    public class Students
    {
        public int STT { get; set; }
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int ClassLevel { get; set; }
        public int Class { get; set; }
    }
}