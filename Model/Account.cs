namespace Model;

public class Account
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public int? UserGroupId { get; set; }

    public virtual ICollection<Role> Roles { get; set; }
}
