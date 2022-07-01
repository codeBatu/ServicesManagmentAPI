using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? UserGroupId { get; set; }

        public virtual UserGroup? UserGroup { get; set; }
    }
}
