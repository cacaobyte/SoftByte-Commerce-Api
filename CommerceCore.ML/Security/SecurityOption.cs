﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.Security
{
    public class SecurityOption
    {
        public int id { get; set; }
        public int menu { get; set; }
        public int? grouper { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string pathIcon { get; set; }
        public string url { get; set; }
        public int orderShow { get; set; }
        public bool active { get; set; }
    }
}
