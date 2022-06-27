using System;
using System.Collections.Generic;

namespace Model
{
    public partial class ServiceTable
    {
        public ServiceTable()
        {
            LogTables = new HashSet<LogTable>();
            MailTables = new HashSet<MailTable>();
        }

        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int? ServiceStatus { get; set; }
        public string? ActiveLife { get; set; }
        public DateTime? RestDateTime { get; set; }
        public int? RestartCount { get; set; }
        public string? Version { get; set; }

        public virtual ICollection<LogTable> LogTables { get; set; }
        public virtual ICollection<MailTable> MailTables { get; set; }
    }
}
