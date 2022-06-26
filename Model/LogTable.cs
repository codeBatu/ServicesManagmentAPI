using System;
using System.Collections.Generic;

namespace Model
{
    public partial class LogTable
    {
        public LogTable()
        {
            MailTables = new HashSet<MailTable>();
        }

        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public string TraceId { get; set; } = null!;
        public string? Contents { get; set; }
        public DateTime? CreateDateTime { get; set; }

        public  ServiceTable? Service { get; set; }
        public  ICollection<MailTable> MailTables { get; set; }
    }
}
