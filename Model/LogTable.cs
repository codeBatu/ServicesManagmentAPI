using System;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{

    public partial class LogTable
    {
        public int Id { get; set; }
        [ForeignKey(nameof(ServiceId))]
        public int ServiceId { get; set; }
        public string? TraceId { get; set; }
        public string? Contents { get; set; }
        public DateTime? CreateDateTime { get; set; } = DateTime.Now;

        public virtual ServiceTable? Service { get; set; }
        public ICollection<MailTable>? MailTables { get; set; }
    }
}

