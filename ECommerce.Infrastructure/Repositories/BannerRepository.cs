using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class BannerRepository : IBannerRepository
{
    private readonly ApplicationDbContext _db;

    public BannerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Banner>> GetByTypeAsync(string type)
        => await _db.Banners
            .Where(x => x.BannerType == type && x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();

    public async Task<IEnumerable<Banner>> GetAllAsync()
        => await _db.Banners.OrderBy(x => x.BannerType).ToListAsync();

    public async Task<Banner?> GetByIdAsync(int id)
        => await _db.Banners.FindAsync(id);

    public async Task<Banner> CreateAsync(Banner banner)
    {
        _db.Banners.Add(banner);
        await _db.SaveChangesAsync();
        return banner;
    }

    public async Task UpdateAsync(Banner banner)
    {
        _db.Banners.Update(banner);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Banner banner)
    {
        _db.Banners.Remove(banner);
        await _db.SaveChangesAsync();
    }
}

