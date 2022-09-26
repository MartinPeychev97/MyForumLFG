using Microsoft.AspNetCore.Identity;

namespace Services.Models.Responses;

public class AuthenticateResponse: SignInResult
{
    public AuthenticateResponse(string token)
    {
        Token = token;
    }
    public string Token { get; set; }
    public bool Unautorised { get; set; }
}
