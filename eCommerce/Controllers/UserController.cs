using System.Reflection.Metadata.Ecma335;
using eCommerce.Contracts.User;
using eCommerce.Models;
using eCommerce.Services;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost, Authorize]
    public IActionResult createUser(CreateUserRequest request)
    {
        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
        Console.WriteLine(passwordHash);

        var user = new User(
            Guid.NewGuid(),
            request.username,
            request.email,
            passwordHash
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

    [HttpPost("{login}")]
    public IActionResult login(LoginRequest request)
    {
        var login = new Login(
            request.username,
            request.password
        );

        var loginSuccess = userService.login(login);
        if (loginSuccess.Length == 0) {
            var response = new LoginResponse(
                false,
                ""
            );
            return BadRequest(response);
        }

        var successResponse = new LoginResponse(
            true,
            loginSuccess
        );  
        return Ok(successResponse);
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