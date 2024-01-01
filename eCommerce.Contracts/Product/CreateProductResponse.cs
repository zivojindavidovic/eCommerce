namespace eCommerce.Contracts.Product;

public record CreateProductResponse(
    Guid productId,
    string name,
    string description,
    double price
);