using System;
using System.Collections.Generic;

namespace DL;

public partial class Transacction
{
    public int Idtransaction { get; set; }

    public string? Fromaccount { get; set; }

    public string? Toaccount { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? SentAt { get; set; }
}
