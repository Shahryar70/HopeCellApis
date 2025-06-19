using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class ManageDonation
{
    public int Id { get; set; }

    public string Donor { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly Date { get; set; }
}
