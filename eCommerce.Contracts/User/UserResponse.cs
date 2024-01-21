namespace eCommerce.Contracts.User;

public record UserResponse(
    bool success,
    Guid userId,
    string username,
    string email
);
