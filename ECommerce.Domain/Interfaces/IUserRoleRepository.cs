using ECommerce.Domain.Entities;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IUserRoleRepository
    {
        Task AddAsync(UserRole userRole);
        Task<bool> UserHasRole(int userId, int roleId);
    }
}
