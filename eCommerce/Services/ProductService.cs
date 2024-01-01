using eCommerce.Models;

namespace eCommerce.Services;

public interface ProductService
{
    bool createProduct(Product product);
    bool orderProduct(Order order);
    List<Order> getOrders(string userId);
    bool processOrder(ProcessOrder processOrder);
    List<Dictionary<string, string>> getProducts();
}