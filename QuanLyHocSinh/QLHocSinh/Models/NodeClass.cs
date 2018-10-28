using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHocSinh.Models
{
    public class NodeClass
    {
        public int iD;
        public string text;
        public string href;
        public string tags;
        public List<NodeClass> nodes;
    }
}