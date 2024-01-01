namespace eCommerce.Contracts.Product;

public record OrderProductRequest(
    string userId,
    string user2Id,
    string productId
);