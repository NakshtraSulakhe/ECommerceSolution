using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Role role)
        {
            await _db.Roles.AddAsync(role);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
            => await _db.Roles.ToListAsync();

        public async Task<Role?> GetByIdAsync(int id)
            => await _db.Roles.FindAsync(id);

        public async Task<Role?> GetByNameAsync(string name)
            => await _db.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }
}
