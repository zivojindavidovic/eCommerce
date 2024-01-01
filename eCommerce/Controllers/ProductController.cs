using eCommerce.Contracts.Product;
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
        
        if (!productService.createProduct(product)) {
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

    [HttpGet("getAll")]
    public IActionResult getProducts()
    {
        return Ok(productService.getProducts());
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

        if (!productService.orderProduct(order)) {
            return BadRequest();
        }

        var response = new OrderProductResponse(
            true,
            order.orderId.ToString()
        );

        return Ok(response);
    }

    [HttpPost("getOrders")]
    public IActionResult getOrders(GetOrdersRequest request)
    {
        var orders = productService.getOrders(request.userId);

        if (orders.Count == 0) {
            return BadRequest("There is no items");
        }

        return Ok(orders);
    }

    [HttpPost("processOrder")]
    public IActionResult processOrder(ProcessOrderRequest request)
    {
        var processOrder = new ProcessOrder(
            request.orderId,
            request.status
        );

        return productService.processOrder(processOrder) ? Ok("Done") : BadRequest("Error");
    }
}