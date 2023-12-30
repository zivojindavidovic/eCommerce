namespace eCommerce.Models;

public class Login
{
    public string username { get; }
    public string password { get; }

    public Login(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}