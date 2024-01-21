namespace eCommerce.Contracts.User;

public record CreateUserResponse(
    bool success,
    Guid id,
    string username,
    string email,
    string password
)
{
    public CreateUserResponse() : this(false, Guid.Empty, "", "", "")
    {
    }
};