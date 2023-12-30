using eCommerce.Contracts.User;
using eCommerce.Models;

namespace eCommerce.Services;

public interface UserService
{
    void createUser(User user);
    User getUser(Guid id);
}