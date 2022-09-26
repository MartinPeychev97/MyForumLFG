using AutoMapper;
using Services.Interfaces;
using Services.Models.Requests;
using Services.Models.Responses;

namespace API.Services.Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUserRepository repository;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<BaseResponse> Create(CreateUserRequest request)
        => await repository.Create(request);

    public async Task<BaseResponse> Update(UpdateUserRequest request)
        => await repository.Update(request);

    public async Task Delete(string email)
        => await repository.Delete(email);

    public async Task<UserResponse> GetUserById(string email)
        => await repository.GetUserById(email);

    public async Task<IReadOnlyCollection<UserResponse>> GetAll()
        => await repository.GetAll();
}
