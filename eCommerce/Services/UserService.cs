using eCommerce.Contracts.User;
using eCommerce.Models;

namespace eCommerce.Services;

public interface UserService
{
    bool createUser(User user);

    string login(Login login);
    User getUser(Guid id);

}