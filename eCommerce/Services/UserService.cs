using eCommerce.Contracts.User;
using eCommerce.Models;

namespace eCommerce.Services;

public interface UserService
{
    bool createUser(User user);
    List<Dictionary<string, object>> login(Login login);
    User getUser(Guid id);
    User upsertUser(Guid id, string username);
}