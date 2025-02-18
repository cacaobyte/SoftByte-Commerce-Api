using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.Security
{
    public class SecurityRoleUser
    {
        public byte role { get; set; }
        public string user { get; set; }
        public List<byte> roles { get; set; }
        public bool superUser { get; set; }
    }
}
