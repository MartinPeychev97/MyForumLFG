using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var users = await userService.GetAll();
        return Ok(users);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userService.GetUserById(id);
        return Ok(user);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateUserRequest model)
    {
        var user = await userService.Create(model);
        return Ok(user);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest model)
    {
        var user = await userService.Update(id, model);
        return Ok(user);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await userService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}
