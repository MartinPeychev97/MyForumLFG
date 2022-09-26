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
    public async Task<IActionResult> GetById(string email)
    {
        var user = await userService.GetUserById(email);
        return Ok(user);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var user = await userService.Create(request);
        return Ok(user);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var user = await userService.Update( request);
        return Ok(user);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string email)
    {
        await userService.Delete(email);
        return Ok(new { message = "User deleted" });
    }
}
