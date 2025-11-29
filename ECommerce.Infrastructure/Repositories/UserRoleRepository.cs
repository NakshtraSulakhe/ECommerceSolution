using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(UserRole userRole)
        {
            await _db.UserRoles.AddAsync(userRole);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> UserHasRole(int userId, int roleId)
        {
            return await _db.UserRoles.AnyAsync(
                x => x.UserId == userId && x.RoleId == roleId
            );
        }
    }
}
