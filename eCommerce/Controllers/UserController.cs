using System.Web.Http.Cors;
using eCommerce.Contracts.User;
using eCommerce.Models;
using eCommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers;

[ApiController]
[Route("users")]
[EnableCors(origins: "http://localhost:4200/", headers: "*", methods: "*")]
public class UserController : ControllerBase
{
    private readonly UserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public IActionResult createUser(CreateUserRequest request)
    {
        int salt = 12;
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password, salt);

        var user = new User(
            Guid.NewGuid(),
            request.username,
            request.email,
            passwordHash
        );

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
        var login = new Login(request.username, request.password);
        var result = userService.login(login);

        if (result.Count > 0)
        {
            if ((string)result[0]["errors"] != "")
            {
                return BadRequest(new LoginResponse(false, (string)result[0]["errors"]));
            }

            string token = (string)result[0]["token"];

            var successResponse = new LoginResponse(true, "", token, (string)result[0]["user_id"], (string)result[0]["username"]);

            return Ok(successResponse);
        }

        return Ok(new LoginResponse(false, ""));
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
        var username = request.username;

        User user = userService.upsertUser(id, username);

        var response = new CreateUserResponse(
            user.id,
            user.username,
            user.email,
            user.password
        );

        return Ok(response);
    }
}