using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eCommerce.External.Database;
using eCommerce.Models;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.Services;

public class UserServiceImpl : UserService
{
    private readonly Dictionary<Guid, User> users = new();
    private readonly Database database;
    private readonly IConfiguration configuration;
    public UserServiceImpl(Database database, IConfiguration configuration)
    {
        this.database = database;
        this.configuration = configuration;
    }

    public bool createUser(User user)
    {
        string sql = $"INSERT INTO user (user_id, username, email, password) VALUES ('{user.id}', '{user.username}', '{user.email}', '{user.password}')";
        bool ret = true;

        if (!database.insert(sql)) {
            ret = false;
        }
        return ret;
    }

    public string login(Login login)
    {
        string sql = $"SELECT * FROM user WHERE username = '{login.username}'";
        var password = "";
               
        var result = database.select(sql);
        var user = result[0];
        
        if (user.ContainsKey("password")) {
            password = user["password"];
        }

        if (!BCrypt.Net.BCrypt.Verify(login.password, password)) {
            return "";
        }

        string token = generateJwtToken(login);

        return token;
    }

    public User getUser(Guid id)
    {
        return users[id];
    }

    private string generateJwtToken(Login login)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, login.username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration.GetSection("AppSettings:Token").Value!
        ));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
       
        var token = new JwtSecurityToken(
            claims: claims, 
            expires: DateTime.Now.AddDays(1), 
            signingCredentials: credentials
            );
        
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }
}