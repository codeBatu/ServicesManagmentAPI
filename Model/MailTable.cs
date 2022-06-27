using System;
using System.Collections.Generic;

namespace Model
{
    public partial class MailTable
    {
        public int Id { get; set; }
        public string? Cc { get; set; }
        public string? Body { get; set; }
        public string Topic { get; set; } = null!;
        public string Gmail { get; set; } = null!;
        public string? Sender { get; set; }
        public int? LogId { get; set; }

        public LogTable? Log { get; set; }
    }
}
