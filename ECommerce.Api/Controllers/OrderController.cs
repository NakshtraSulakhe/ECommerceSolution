using System.Security.Claims;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;

    public OrderController(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("place")]
    public async Task<IActionResult> PlaceOrder()
    {
        var userId = GetUserId();
        var order = await _orderRepo.PlaceOrderAsync(userId);
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = GetUserId();
        var orders = await _orderRepo.GetUserOrdersAsync(userId);
        return Ok(orders);
    }
}
