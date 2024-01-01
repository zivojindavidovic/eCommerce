namespace eCommerce.Models;

public class ProcessOrder
{
    public string orderId { get; }
    public string status { get; }

    public ProcessOrder(string orderId, string status)
    {
        this.orderId = orderId;
        this.status = status;
    }
}