using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class CMSPage

    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Slug { get; set; } = "";   // URL friendly name
        public string Content { get; set; } = ""; // HTML content
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
