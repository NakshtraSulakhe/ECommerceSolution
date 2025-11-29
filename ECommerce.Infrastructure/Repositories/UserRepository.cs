using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmail(string email)
            => await _db.Users.AnyAsync(x => x.Email == email);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _db.Users.Include(u => u.UserRoles).ToListAsync();

        public async Task<User?> GetByEmailAsync(string email)
            => await _db.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetByIdAsync(int id)
            => await _db.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
