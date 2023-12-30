namespace eCommerce.Models;

public class User
{
    public Guid id { get; }
    public string username { get; }
    public string email { get; }
    public string password { get; }

    public User(Guid id, string username, string email, string password)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.password = password;
    }
}