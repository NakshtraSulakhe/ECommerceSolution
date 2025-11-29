using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetByTypeAsync(string type);
        Task<IEnumerable<Banner>> GetAllAsync();
        Task<Banner?> GetByIdAsync(int id);
        Task<Banner> CreateAsync(Banner banner);
        Task UpdateAsync(Banner banner);
        Task DeleteAsync(Banner banner);
    }
}
