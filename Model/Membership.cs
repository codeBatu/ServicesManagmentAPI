using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Membership
    {
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public int? GroupAdminId { get; set; }

        public virtual UserGroup? Group { get; set; }
        public virtual Account? GroupAdmin { get; set; }
        public virtual Account? User { get; set; }
    }
}
