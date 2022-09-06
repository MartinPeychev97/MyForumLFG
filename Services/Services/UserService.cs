using API.DataAccess;
using API.Services.ServiceContracts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Repositories;
using Services.Requests;
using AutoMapper;


namespace API.Services.Services;

public class UserService : IUserService
{
    private DBContext context;
    private readonly IMapper mapper;

    public UserService(DBContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Create(CreateUserRequest request)
    {
        // validate
        if (context.Users.Any(x => x.Email == request.Email))
            throw new Exception("User with the email '" + request.Email + "' already exists");

        // map model to new user object
        var user = mapper.Map<UserEntity>(request);

        // hash password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        // save user
        context.Users.Add(user);
        context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var user = GetById(id);
        context.Users.Remove(user);
        context.SaveChanges();
    }


    public void Update(Guid id, UpdateUserRequest request)
    {
        var user = GetById(id);

        // validate
        if (request.Email != user.Email && context.Users.Any(x => x.Email == request.Email))
            throw new Exception("User with the email '" + request.Email + "' already exists");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // copy model to user and save
        mapper.Map(request, user);
        context.Users.Update(user);
        context.SaveChanges();
    }

    public UserEntity GetById(Guid id)
    {
        var user = context.Users.Find(id);
        if (user == null) 
            throw new KeyNotFoundException("User not found");
        return user;
    }
    public IEnumerable<UserEntity> GetAll()
    {
        return context.Users;
    }
    
}
