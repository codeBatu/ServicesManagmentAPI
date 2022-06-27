using System;
using System.Collections.Generic;

namespace Model.EntityDto
{
    public  class MailTableDtoEntity
    {
     
        public string? Content { get; set; }
        public int? LogId { get; set; }
        public int? ServicesId { get; set; }

        public virtual LogTable? Log { get; set; }
        public virtual ServiceTableDtoEntity? Services { get; set; }
    }
}
