using eCommerce.Models;

namespace eCommerce.Services;

public interface ProductService
{
    bool createProduct(Product product);
    bool orderProduct(Order order);
    List<Order> getOrders(string userId);
    bool processOrder(ProcessOrder processOrder);
    List<Dictionary<string, string>> getProducts();
    List<Dictionary<string, string>> getMyProducts(string userId);
    bool deleteProduct(string id);

    Dictionary<string, string> getSingleProduct(string id);

    bool updateProduct(string id, double price);
}