using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Banner
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string BannerType { get; set; } = "";  // home, category, offer, etc.
        public int? CategoryId { get; set; }          // optional (for category-specific banners)

        public string ImageUrl { get; set; } = "";

        public string? RedirectUrl { get; set; }      // optional click action
        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }
}
