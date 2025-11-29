using ECommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id);
        Task<Role?> GetByNameAsync(string name);
        Task<IEnumerable<Role>> GetAllAsync();

        Task AddAsync(Role role);
    }
}
