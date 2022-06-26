using System;
using System.Collections.Generic;

namespace Model.EntityDto
{
    public  class ServiceTableDtoEntity
    {
        public ServiceTableDtoEntity()
        {
            LogTables = new HashSet<LogTable>();
            MailTables = new HashSet<MailTableDtoEntity>();
        }

      
        public string? ServiceName { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int? ServiceStatus { get; set; }
        public string? ActiveLife { get; set; }
        public DateTime? RestDateTime { get; set; }
        public int? RestartCount { get; set; }
        public string? Version { get; set; }

        public virtual ICollection<LogTable> LogTables { get; set; }
        public virtual ICollection<MailTableDtoEntity> MailTables { get; set; }
    }
}
