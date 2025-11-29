using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly ICartRepository _cartRepo;
        public OrderRepository(ApplicationDbContext db, ICartRepository cartRepo)
        {
            _db = db;
            _cartRepo = cartRepo;

        }

        public async Task<Order> PlaceOrderAsync(int userId)
        {
            var cart = await _cartRepo.GetUserCartAsync(userId);

            if(!cart.Items.Any())
                throw new Exception("Cart is empty");

            // Create order

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = cart.Items.Sum(i => i.Product.Price * i.Quantity),
               
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            foreach (var item in cart.Items)
            {
                _db.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product!.Price
                });
            }

            await _db.SaveChangesAsync();


            // Clear cart
            _db.CartItems.RemoveRange(cart.Items);
            await _db.SaveChangesAsync();

            return order;

        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _db.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
