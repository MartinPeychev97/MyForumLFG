using Services.Requests;

namespace Services.Repositories
{
    public interface IUserRepository
    {
        Task<string> GenerateJwtToken(LoginUserRequest user);
        //Task<AuthenticateResponse> RegisterAsync(RegisterUserRequest request);
        Task<AuthenticateResponse> Login(LoginUserRequest userInput);
        Task Logout();

    }
}