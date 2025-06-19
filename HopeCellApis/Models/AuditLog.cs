using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class AuditLog
{
    public int LogId { get; set; }

    public string ActionType { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public int RecordId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? ChangedBy { get; set; }

    public DateTime ChangeDate { get; set; }
}
