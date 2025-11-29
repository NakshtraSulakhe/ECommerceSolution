using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class CMSPageDto
    {
        public string Title { get; set; } = "";
        public string Slug { get; set; } = "";
        public string Content { get; set; } = "";
    }
}
