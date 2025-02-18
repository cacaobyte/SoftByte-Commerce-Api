using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.Security
{
    public class SecurityRoleOption
    {
        public int id { get; set; }
        public int role { get; set; }
        public List<int> options { get; set; } = new List<int>();  
        public List<int> roles { get; set; } = new List<int>(); 
        public bool allowed { get; set; }
    }
}

