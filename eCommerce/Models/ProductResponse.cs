namespace eCommerce.Models;

public class ProductResponse
{
    public string productId { get; }
    public string userId { get; }
    public string name { get; }
    public string description { get; }
    public double price { get; }

    public ProductResponse(string productId, string userId, string name, string description, double price)
    {
        this.productId = productId;
        this.userId = userId;
        this.name = name;
        this.description = description;
        this.price = price;
    }
}