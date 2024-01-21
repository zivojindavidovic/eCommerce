namespace eCommerce.Contracts.User;

public record UpsertUseResponse(
    Guid userId,
    string username,
    string email,
    string password
);