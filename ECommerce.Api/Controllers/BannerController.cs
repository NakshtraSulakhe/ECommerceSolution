using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BannerController : ControllerBase
{
    private readonly IBannerRepository _repo;

    public BannerController(IBannerRepository repo)
    {
        _repo = repo;
    }

    // --------------------------
    // PUBLIC: Get banners by type
    // --------------------------
    [AllowAnonymous]
    [HttpGet("{type}")]
    public async Task<IActionResult> GetByType(string type)
    {
        return Ok(await _repo.GetByTypeAsync(type.ToLower()));
    }

    // --------------------------
    // ADMIN: Create Banner
    // --------------------------
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] BannerDto dto,
                                            [FromForm] IFormFile image,
                                            [FromServices] ImageHelper helper)
    {
        if (image == null)
            return BadRequest("Banner image required");

        string imageUrl = await helper.SaveBannerImageAsync(image, dto.BannerType);

        var banner = new Banner
        {
            Title = dto.Title,
            BannerType = dto.BannerType.ToLower(),
            RedirectUrl = dto.RedirectUrl,
            CategoryId = dto.CategoryId,
            DisplayOrder = dto.DisplayOrder,
            ImageUrl = imageUrl,
            IsActive = true
        };

        await _repo.CreateAsync(banner);
        return Ok(banner);
    }

    // --------------------------
    // ADMIN: Update Banner
    // --------------------------
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BannerUpdateDto dto)
    {
        var banner = await _repo.GetByIdAsync(id);
        if (banner == null)
            return NotFound();

        banner.Title = dto.Title;
        banner.BannerType = dto.BannerType.ToLower();
        banner.RedirectUrl = dto.RedirectUrl;
        banner.CategoryId = dto.CategoryId;
        banner.DisplayOrder = dto.DisplayOrder;
        banner.IsActive = dto.IsActive;

        await _repo.UpdateAsync(banner);
        return Ok(banner);
    }

    // --------------------------
    // ADMIN: Delete
    // --------------------------
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var banner = await _repo.GetByIdAsync(id);
        if (banner == null)
            return NotFound();

        await _repo.DeleteAsync(banner);
        return Ok("Banner deleted");
    }
}
