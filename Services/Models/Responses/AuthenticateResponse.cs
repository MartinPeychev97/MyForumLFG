namespace Services.Models.Responses;

public class AuthenticateResponse
{
    public AuthenticateResponse(string token)
    {
        Token = token;
    }
    public string Token { get; set; }
}
