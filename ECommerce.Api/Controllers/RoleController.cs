using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepo;
        private readonly IUserRepository _userRepo;
        private readonly IUserRoleRepository _userRoleRepo;

        public RoleController(IRoleRepository roleRepo, IUserRepository userRepo, IUserRoleRepository userRoleRepo)
        {
            _roleRepo = roleRepo;
            _userRepo = userRepo;
            _userRoleRepo = userRoleRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(RoleDto dto)
        {
            if (await _roleRepo.GetByNameAsync(dto.RoleName) != null)
                return BadRequest("Role already exists.");

            var role = new Role { Name = dto.RoleName };
            await _roleRepo.AddAsync(role);

            return Ok(new { message = "Role created", role });
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            var user = await _userRepo.GetByIdAsync(dto.UserId);
            if (user == null) return NotFound("User not found");

            var role = await _roleRepo.GetByIdAsync(dto.RoleId);
            if (role == null) return NotFound("Role not found");

            if (await _userRoleRepo.UserHasRole(dto.UserId, dto.RoleId))
                return BadRequest("Role already assigned.");

            await _userRoleRepo.AddAsync(new UserRole
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId
            });

            return Ok("Role assigned.");
        }
    }
}
