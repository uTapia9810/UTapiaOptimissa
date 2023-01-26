using System;
using System.Collections.Generic;

namespace DL;

public partial class Account
{
    public string Account1 { get; set; } = null!;

    public decimal? Balance { get; set; }

    public string? Owner { get; set; }

    public DateTime? CreatedAt { get; set; }
}
