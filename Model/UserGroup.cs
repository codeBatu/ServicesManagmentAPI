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

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
