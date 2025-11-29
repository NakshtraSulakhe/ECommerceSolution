using ECommerce.Application.DTOs;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // user must be logged in
public class CartController : ControllerBase
{
    private readonly ICartRepository _repo;

    public CartController(ICartRepository repo)
    {
        _repo = repo;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userId = GetUserId();
        var cart = await _repo.GetUserCartAsync(userId);
        return Ok(cart);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(AddToCartDto dto)
    {
        var userId = GetUserId();
        await _repo.AddItemAsync(userId, dto.ProductId, dto.Quantity);
        return Ok("Item added");
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateItem(UpdateCartItemDto dto)
    {
        var userId = GetUserId();
        await _repo.UpdateItemAsync(userId, dto.ProductId, dto.Quantity);
        return Ok("Cart updated");
    }

    [HttpDelete("remove/{productId}")]
    public async Task<IActionResult> RemoveItem(int productId)
    {
        var userId = GetUserId();
        await _repo.RemoveItemAsync(userId, productId);
        return Ok("Item removed");
    }
}
