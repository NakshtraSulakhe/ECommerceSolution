using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Product
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category?  Category { get; set; }
    }
}
