using System;
using System.Collections.Generic;

namespace Model;

public partial class LogTable
{
    public int Id { get; set; }
    public int? ServiceId { get; set; }
    public string TraceId { get; set; } = null!;
    public string? Contents { get; set; }
    public DateTime? CreateDateTime { get; set; } = DateTime.Now;

    public virtual ServiceTable? Service { get; set; }
}
