namespace Model.DTOs.Accounts;
using Model;

public class UserWithPermissions
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

    public bool? CanUpdate { get; set; } = false;
    public bool? CanCreate { get; set; } = false;
    public bool? CanRemove { get; set; } = false;
    public bool? CanGetAll { get; set; } = false;
    public bool? CanActive { get; set; } = false;
    public bool? CanInActive { get; set; } = false;
    public bool? CanRestart { get; set; } = false;
}
