namespace Model.DTOs.Accounts;

public class AccountResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Model.Role Role { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public int? UserGroupId { get; set; }
}
