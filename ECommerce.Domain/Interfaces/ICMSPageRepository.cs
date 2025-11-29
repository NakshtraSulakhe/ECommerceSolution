using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface ICMSPageRepository
    {
        Task<IEnumerable<CMSPage>> GetAllAsync();
        Task<CMSPage?> GetByIdAsync(int id);
        Task<CMSPage?> GetBySlugAsync(string slug);
        Task<CMSPage> CreateAsync(CMSPage page);
        Task UpdateAsync(CMSPage page);
        Task DeleteAsync(CMSPage page);
    }

}
