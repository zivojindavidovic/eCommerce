namespace eCommerce.Contracts.User;

public record CreateUserResponse(
    Guid id,
    string username,
    string email,
    string password
);