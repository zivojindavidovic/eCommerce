namespace eCommerce.Contracts.User;

public record LoginResponse(
    bool success,
    string token
);