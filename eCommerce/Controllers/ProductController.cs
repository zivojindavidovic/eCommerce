using eCommerce.Contracts.Product;
using eCommerce.Contracts.User;
using eCommerce.Models;
using eCommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly ProductService productService;

    public ProductController(ProductService productService)
    {
        this.productService = productService;
    }

    [HttpPost, Authorize]
    public IActionResult createProduct(CreateProductRequest request)
    {
        var product = new Product(
            Guid.NewGuid(),
            request.userId,
            request.name,
            request.description,
            request.price
        );

        if (!productService.createProduct(product))
        {
            return BadRequest();
        }

        var response = new CreateProductResponse(
            product.productId,
            product.name,
            product.description,
            product.price
        );

        return Ok(response);
    }

    [HttpGet("getSingle/{id:guid}")]
    public IActionResult getSingle(Guid id)
    {
        return Ok(productService.getSingleProduct(id.ToString()));
    }

    [HttpDelete("delete/{id:guid}")]
    public IActionResult deleteProduct(Guid id)
    {

        return productService.deleteProduct(id.ToString()) ? Ok(new OrderResponse(true)) : BadRequest(new OrderResponse(false));
    }

    [HttpGet("getAll")]
    public IActionResult getProducts()
    {
        return Ok(productService.getProducts());
    }

    [HttpPut("update/{id:guid}")]
    public IActionResult updateProduct(Guid id, UpserProductRequest request)
    {
        var dPrice = Double.Parse(request.price);
        return productService.updateProduct(id.ToString(), dPrice) ? Ok(new OrderResponse(true)) : BadRequest(new OrderResponse(false));
    }

    [HttpGet("myProducts/{id:guid}")]
    public IActionResult getMyProducts(Guid id)
    {
        return Ok(productService.getMyProducts(id.ToString()));
    }

    [HttpGet("getOrders/{id:guid}")]
    public IActionResult getOrders(Guid id)
    {
        var userId = id.ToString();
        var orders = productService.getOrders(userId.ToString());

        if (orders.Count == 0)
        {
            return BadRequest("There is no items");
        }

        return Ok(orders);
    }

    [HttpPost("order")]
    public IActionResult orderProduct(OrderProductRequest request)
    {
        var order = new Order(
            Guid.NewGuid().ToString(),
            request.userId,
            request.user2Id,
            request.productId,
            "Pending"
        );

        if (!productService.orderProduct(order))
        {
            return BadRequest();
        }

        var response = new OrderProductResponse(
            true,
            order.orderId.ToString()
        );

        return Ok(response);
    }

    [HttpPost("processOrder")]
    public IActionResult processOrder(ProcessOrderRequest request)
    {
        var processOrder = new ProcessOrder(
            request.orderId,
            request.status
        );

        return productService.processOrder(processOrder) ? Ok(new OrderResponse(true)) : BadRequest(new OrderResponse(false));
    }
}