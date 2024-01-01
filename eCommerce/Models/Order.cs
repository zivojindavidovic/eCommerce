namespace eCommerce.Models;

public class Order
{
    public string orderId { get; }
    public string userId { get; }
    public string user2Id { get ; }
    public string productId { get; }
    public string status { get ;}

    public Order(string orderId, string userId, string user2Id, string productId, string status)
    {
        this.orderId = orderId;
        this.userId = userId;
        this.user2Id = user2Id;
        this.productId = productId;
        this.status = status;
    }
}