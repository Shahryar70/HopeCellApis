﻿using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }
}
