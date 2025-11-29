using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; } = "";

        public string Slug { get; set; } = ""; // for SEO-friendly URLs

        public List<Product>? Products { get; set; }
    }
}
