namespace eCommerce.Models;

public class Product
{
    public Guid productId;
    public string userId;
    public string name { get; }
    public string description { get; }
    public double price { get; }

    public Product(Guid productId, string userId, string name, string description, double price)
    {
        this.productId = productId;
        this.userId = userId;
        this.name = name;
        this.description = description;
        this.price = price;
    }
}