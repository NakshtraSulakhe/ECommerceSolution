using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CMSPageRepository : ICMSPageRepository
{
    private readonly ApplicationDbContext _db;

    public CMSPageRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CMSPage>> GetAllAsync()
        => await _db.CMSPages.ToListAsync();

    public async Task<CMSPage?> GetByIdAsync(int id)
        => await _db.CMSPages.FindAsync(id);

    public async Task<CMSPage?> GetBySlugAsync(string slug)
        => await _db.CMSPages.FirstOrDefaultAsync(x => x.Slug == slug);

    public async Task<CMSPage> CreateAsync(CMSPage page)
    {
        _db.CMSPages.Add(page);
        await _db.SaveChangesAsync();
        return page;
    }

    public async Task UpdateAsync(CMSPage page)
    {
        _db.CMSPages.Update(page);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(CMSPage page)
    {
        _db.CMSPages.Remove(page);
        await _db.SaveChangesAsync();
    }
}
