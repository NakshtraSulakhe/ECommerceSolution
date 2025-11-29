using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; } = "";

        public string Email { get; set; } = "";

        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}
