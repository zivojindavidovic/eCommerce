namespace eCommerce.Contracts.Product;

public record ProcessOrderRequest(
    string orderId,
    string status
);