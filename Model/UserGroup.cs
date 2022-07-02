using System;
using System.Collections.Generic;

namespace Model
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public string? Member { get; set; }
        public string? Admin { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
