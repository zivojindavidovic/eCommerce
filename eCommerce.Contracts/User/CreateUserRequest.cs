namespace eCommerce.Contracts.User;

public record CreateUserRequest(
    string username,
    string email,
    string password
);