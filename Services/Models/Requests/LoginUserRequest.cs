namespace Services.Models.Requests;

public class LoginUserRequest
{
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public bool RememberMe { get; set; }
}
