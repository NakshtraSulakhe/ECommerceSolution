using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;

    public CartRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Cart> GetUserCartAsync(int userId)
    {
        var cart = await _db.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
        }

        return cart;
    }

    public async Task AddItemAsync(int userId, int productId, int qty)
    {
        var cart = await GetUserCartAsync(userId);

        var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (existing != null)
        {
            existing.Quantity += qty;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = productId,
                Quantity = qty
            });
        }

        await _db.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(int userId, int productId, int qty)
    {
        var cart = await GetUserCartAsync(userId);

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            item.Quantity = qty;
            if (qty == 0) cart.Items.Remove(item);
        }

        await _db.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int userId, int productId)
    {
        var cart = await GetUserCartAsync(userId);
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
