using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.Security
{
    public class SecurityUserOption
    {
        public int id { get; set; }
        public string user { get; set; }
        public int option { get; set; }
        public List<int> options { get; set; }
        public bool allowed { get; set; }
    }
}
