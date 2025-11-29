using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> PlaceOrderAsync(int userId);
        Task<List<Order>> GetUserOrdersAsync(int userId);
    }
}
