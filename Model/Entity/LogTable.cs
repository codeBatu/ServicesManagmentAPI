using System;
using System.Collections.Generic;

namespace Model.Entity

{
    public  class LogTable
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

        public virtual ServiceTable? Service { get; set; }
        public virtual ICollection<MailTable> MailTables { get; set; }
    }
}
