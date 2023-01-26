using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Account
    {
        public string account { get; set;  }
        public decimal balance { get; set; } 
        public string owner { get; set; } 
        public string createdAt { get; set; }
        public List<object> Accounts { get; set; }
    }
}
