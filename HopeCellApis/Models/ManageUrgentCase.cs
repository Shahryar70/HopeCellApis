using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class ManageUrgentCase
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Patient { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Deadline { get; set; } = null!;

    public string Priority { get; set; } = null!;
}
