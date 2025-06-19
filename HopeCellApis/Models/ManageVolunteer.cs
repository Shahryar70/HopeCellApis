using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class ManageVolunteer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly Joined { get; set; }
}
