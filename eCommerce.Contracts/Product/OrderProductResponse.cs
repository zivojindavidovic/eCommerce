namespace eCommerce.Contracts.Product;

public record OrderProductResponse(
    bool success,
    string orderId
);