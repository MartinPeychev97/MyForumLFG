using API.Services.ServiceContracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService userService;
    private IMapper mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        this.userService = userService;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var user = userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(CreateUserRequest model)
    {
        userService.Create(model);
        return Ok(new { message = "User created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, UpdateUserRequest model)
    {
        userService.Update(id, model);
        return Ok(new { message = "User updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        userService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}
