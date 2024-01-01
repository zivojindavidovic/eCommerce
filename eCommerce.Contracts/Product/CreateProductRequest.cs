namespace eCommerce.Contracts.Product;

public record CreateProductRequest(
    string userId,
    string name,
    string description,
    double price
);