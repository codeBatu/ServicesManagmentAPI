using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? UserGroupId { get; set; }
        public Role? Role { get; set; }

        public  UserGroup? UserGroup { get; set; }
        public  GroupAccount GroupAccount { get; set; } = null!;
    }
}
