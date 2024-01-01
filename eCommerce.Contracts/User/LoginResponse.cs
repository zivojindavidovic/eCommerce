namespace eCommerce.Contracts.User;

public record LoginResponse(
    bool success,
    string errors,
    string token,
    string userId,
    string username
)
{
    public LoginResponse(bool success, string errors): this(success, errors, "", "", "")
    {
    }
}
