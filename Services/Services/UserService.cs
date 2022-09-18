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

    public async Task<UserResponse> Create(CreateUserRequest request)
        => await repository.Create(request);

    public async Task<UserResponse> Update(Guid id, UpdateUserRequest request)
        => await repository.Update(id, request);

    public async Task Delete(Guid id)
        => await repository.Delete(id);

    public async Task<UserResponse> GetUserById(Guid id)
        => await repository.GetUserById(id);

    public async Task<IReadOnlyCollection<UserResponse>> GetAll()
        => await repository.GetAll();
}
