using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetUserCartAsync(int userId);
        Task AddItemAsync(int userId, int productId, int qty);
        Task UpdateItemAsync(int userId, int productId, int qty);
        Task RemoveItemAsync(int userId, int productId);
    }
}
