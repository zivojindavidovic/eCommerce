using eCommerce.Contracts.User;
using eCommerce.Models;
using eCommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpPost()]
    public IActionResult createUser(CreateUserRequest request)
    {
        var user = new User(
            Guid.NewGuid(),
            request.username,
            request.email,
            request.password
        );

        //Save to DB
        userService.createUser(user);

        var response = new CreateUserResponse(
            user.id,
            user.username, 
            user.email,
            user.password
        );

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult getUser(Guid id)
    {
        User user = userService.getUser(id);

        var response = new CreateUserResponse(
            user.id,
            user.username,
            user.email,
            user.password
        );
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult upsertUser(Guid id, UpsertUserRequest request)
    {

        return Ok(request);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult deleteUser(Guid id)
    {

        return Ok(id);
    }
}