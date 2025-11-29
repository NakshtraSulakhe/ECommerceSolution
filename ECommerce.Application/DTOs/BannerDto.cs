using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class BannerDto
    {
        public string Title { get; set; } = "";
        public string BannerType { get; set; } = "";
        public string? RedirectUrl { get; set; }
        public int? CategoryId { get; set; }
        public int DisplayOrder { get; set; } = 0;
    }
}
