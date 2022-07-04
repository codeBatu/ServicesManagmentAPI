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
        public string? GroupName { get; set; } 


        public  ICollection<Account> Accounts { get; set; }
    }
}
