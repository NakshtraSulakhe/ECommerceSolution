using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure.Services
{
    public class AuthService : IAuthService

    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IRoleRepository _roleRepo;
        public AuthService(ApplicationDbContext db, IConfiguration config,
                       IUserRoleRepository userRoleRepo, IRoleRepository roleRepo)
        {
            _db = db;
            _config = config;
            _userRoleRepo = userRoleRepo;
            _roleRepo = roleRepo;
        }

        public async Task<User?> Register(string fullName, string email, string password)
        {
            if (await _db.Users.AnyAsync(u => u.Email == email))
                return null;

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }
        public async Task<User?> Login(string email, string password)
        {
            var user = await _db.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            // Add user roles
            foreach (var role in user.UserRoles.Select(r => r.Role!.Name))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(hash);
        }

    }
}
