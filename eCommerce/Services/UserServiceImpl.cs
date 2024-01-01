using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
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

        if (!database.insert(sql))
        {
            ret = false;
        }
        return ret;
    }

    public List<Dictionary<string, object>> login(Login login)
    {
        List<Dictionary<string, object>> ret = new List<Dictionary<string, object>>();
        Dictionary<string, object> retValue = new Dictionary<string, object>();
        retValue["success"] = false;
        retValue["errors"] = "There is not such a user";
        retValue["token"] = "";

        string sql = $"SELECT * FROM user WHERE username = '{login.username}'";
        var password = "";

        var result = database.select(sql);
        if (result.Count > 0)
        {
            var user = result[0];
            if (user.ContainsKey("password"))
            {
                password = user["password"];
            }

            if (!BCrypt.Net.BCrypt.Verify(login.password, password))
            {
                retValue["errors"] = "Wrong credentials";
            }
            else
            {
                string token = generateJwtToken(login);
                retValue["errors"] = "";
                retValue["success"] = true;
                retValue["token"] = token;
                retValue["user_id"] = user["user_id"];
                retValue["username"] = user["username"];

            }
        }



        ret.Add(retValue);

        return ret;
    }

    public User getUser(Guid id)
    {
        string sql = $"SELECT * FROM user WHERE user_id = '{id}'";
        var result = database.select(sql);
        var usr = result[0];
        var user = new User(Guid.Parse(usr["user_id"]), usr["username"], usr["email"], usr["password"]);

        return user;
    }

    public User upsertUser(Guid id, string username)
    {
        string sql = $"UPDATE user set username = '{username}' WHERE user_id = '{id}'";
        var updateResult = database.update(sql);
        string select = $"SELECT * FROM user WHERE user_id = '{id}'";
        var result = database.select(select);
        var usr = result[0];
        var user = new User(Guid.Parse(usr["user_id"]), usr["username"], usr["email"], usr["password"]);

        return user;
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