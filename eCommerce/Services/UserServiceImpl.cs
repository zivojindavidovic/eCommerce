using eCommerce.Models;

namespace eCommerce.Services;

public class UserServiceImpl : UserService
{
    private readonly Dictionary<Guid, User> users = new();
    public void createUser(User user)
    {
        users.Add(user.id, user);
    }

    public User getUser(Guid id)
    {
        return users[id];
    }
}