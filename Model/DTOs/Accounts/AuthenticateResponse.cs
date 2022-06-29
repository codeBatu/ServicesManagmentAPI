namespace Model.DTOs.Accounts;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<Role> Roles { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public int? GroupId { get; set; }
    public string JwtToken { get; set; }
}
