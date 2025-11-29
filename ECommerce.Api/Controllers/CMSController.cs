using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSController : ControllerBase
    {
        private readonly ICMSPageRepository _repo;

        public CMSController(ICMSPageRepository repo)
        {
            _repo = repo;
        }

        // ---------------------------------------------
        // PUBLIC: Get Page by Slug (Storefront)
        // ---------------------------------------------


        [AllowAnonymous]
        [HttpGet("page/{slug}")]
        public async Task<IActionResult> GetPage(string slug)
        {
            var page = await _repo.GetBySlugAsync(slug);
            if (page == null || !page.IsActive)
                return NotFound();

            return Ok(page);
        }

        // ---------------------------------------------
        // ADMIN: Get All Pages
        // ---------------------------------------------
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPages()
        {
            return Ok(await _repo.GetAllAsync());
        }

        // ---------------------------------------------
        // ADMIN: Create Page
        // ---------------------------------------------
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePage(CMSPageDto dto)
        {
            var page = new CMSPage
            {
                Title = dto.Title,
                Slug = dto.Slug.ToLower().Trim(),
                Content = dto.Content,
                LastUpdated = DateTime.UtcNow,
                IsActive = true
            };

            await _repo.CreateAsync(page);
            return Ok(page);
        }

        // ---------------------------------------------
        // ADMIN: Update Page
        // ---------------------------------------------
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePage(int id, UpdateCMSPageDto dto)
        {
            var page = await _repo.GetByIdAsync(id);
            if (page == null)
                return NotFound();

            page.Title = dto.Title;
            page.Slug = dto.Slug.ToLower().Trim();
            page.Content = dto.Content;
            page.IsActive = dto.IsActive;
            page.LastUpdated = DateTime.UtcNow;

            await _repo.UpdateAsync(page);
            return Ok(page);
        }

        // ---------------------------------------------
        // ADMIN: Delete Page
        // ---------------------------------------------
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            var page = await _repo.GetByIdAsync(id);
            if (page == null)
                return NotFound();

            await _repo.DeleteAsync(page);
            return Ok("Deleted");
        }
    }
}

