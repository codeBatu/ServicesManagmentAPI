using System;
using System.Collections.Generic;

namespace Model
{
    public partial class MailTable
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int? LogId { get; set; }
        public int? ServicesId { get; set; }

        public virtual LogTable? Log { get; set; }
        public virtual ServiceTable? Services { get; set; }
    }
}
