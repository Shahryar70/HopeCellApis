using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class DashboardStat
{
    public int Id { get; set; }

    public int? TotalUrgentCases { get; set; }

    public decimal? DonationsReceived { get; set; }

    public int? RegisteredDonors { get; set; }
}
