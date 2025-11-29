using ECommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();

        Task AddAsync(User user);
        Task UpdateAsync(User user);

        Task<bool> ExistsByEmail(string email);
    }
}
