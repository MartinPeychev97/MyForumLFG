using Microsoft.AspNetCore.Identity;

namespace Services.Models.Responses;

public class AuthenticateResponse : SignInResult
{
    public AuthenticateResponse()
    {

    }
    public AuthenticateResponse(string token)
    {
        Token = token;
    }
    public string Token { get; set; } = string.Empty;
    public bool Unautorised { get; set; }
}
