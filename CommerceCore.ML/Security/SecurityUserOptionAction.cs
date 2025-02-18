using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.Security
{
    public class SecurityUserOptionAction
    {
        public int userOption { get; set; }
        public int action { get; set; }
        public List<byte> userOptions { get; set; }
        public bool allowed { get; set; }
    }
}
