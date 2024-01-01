using eCommerce.External.Database;
using eCommerce.Models;

namespace eCommerce.Services;

public class ProductServiceImpl : ProductService
{
    private readonly Database database;

    public ProductServiceImpl(Database database)
    {
        this.database = database;
    }

    public bool createProduct(Product product)
    {
        string sql = $"INSERT INTO product (product_id, user_id, name, description, price) VALUES('{product.productId}', '{product.userId}', '{product.name}', '{product.description}', {product.price})";
        bool ret = true;

        if (!database.insert(sql))
        {
            ret = false;
        }

        return ret;
    }

    public bool orderProduct(Order order)
    {
        string sql = $"INSERT INTO `order` (order_id, user_id, user2_id, product_id, status) VALUES('{order.orderId}', '{order.userId}', '{order.user2Id}', '{order.productId}', '{order.status}')";
        bool ret = true;

        if (!database.insert(sql))
        {
            ret = false;
        }

        return ret;
    }

    public List<Order> getOrders(string userId)
    {
        List<Order> orders = new List<Order>();
        string sql = $"SELECT * FROM `order` WHERE user2_id = '{userId}' and status = 'pending'";
        
        var result = database.select(sql);

        if (result.Count != 0)
        {
            foreach (var res in result)
            {
                if (res.ContainsKey("order_id") &&
                res.ContainsKey("user_id") &&
                res.ContainsKey("user2_id") &&
                res.ContainsKey("product_id") &&
                res.ContainsKey("status"))
                {
                    string orderId = res["order_id"];
                    string user1Id = res["user_id"];
                    string user2Id = res["user2_id"];
                    string productId = res["product_id"];
                    string status = res["status"];
                    orders.Add(new Order(orderId, user1Id, user2Id, productId, status));
                }
            }
        }

        return orders;
    }

    public bool processOrder(ProcessOrder processOrder)
    {
        string sql = $"UPDATE `order` SET status = '{processOrder.status}' WHERE order_id = '{processOrder.orderId}'";

        var result = database.update(sql);

        return result;
    }

    public List<Dictionary<string, string>> getProducts()
    {
        string sql = $"SELECT * FROM `product`";
        var result = database.select(sql);

        return result;
    }
}