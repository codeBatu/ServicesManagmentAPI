using System;
using System.Collections.Generic;

namespace Model.EntityDto
{
    public class LogTableDtoEntity
    {
        public LogTableDtoEntity()
        {
            MailTables = new HashSet<MailTableDtoEntity>();
        }


        public int? ServiceId { get; set; }
        public string TraceId { get; set; } = null!;
        public string? Contents { get; set; }
        public DateTime? CreateDateTime { get; set; }

        public virtual ServiceTableDtoEntity? Service { get; set; }
        public virtual ICollection<MailTableDtoEntity> MailTables { get; set; }
    }
}
