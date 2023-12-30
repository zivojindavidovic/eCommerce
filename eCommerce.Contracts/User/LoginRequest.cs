namespace eCommerce.Contracts.User;

public record LoginRequest(
    string username,
    string password
);