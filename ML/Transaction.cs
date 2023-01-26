using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Transaction
    {
        public int IdTransaction { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
        public string SentAt { get; set; }
        public List<object> Transactions { get; set; }
    }
}
