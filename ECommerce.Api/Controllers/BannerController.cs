using ECommerce.Api.DTOs;
using ECommerce.Api.Helpers;
using ECommerce.API.Helpers;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/banners")]
public class BannerController : ControllerBase
{
    private readonly IBannerRepository _repo;
    private readonly IWebHostEnvironment _env;

    public BannerController(IBannerRepository repo, IWebHostEnvironment env)
    {
        _repo = repo;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var banners = await _repo.GetAllAsync();
        return Ok(banners);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var banner = await _repo.GetByIdAsync(id);
        if (banner == null) return NotFound();
        return Ok(banner);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] BannerCreateDto dto)
    {
        string imagePath = ImageHelper.SaveBannerImage(dto.ImageFile, _env.WebRootPath);

        var banner = new Banner
        {
            Title = dto.Title,
            BannerType = dto.BannerType,
            DisplayOrder = dto.DisplayOrder,
            IsActive = dto.IsActive,
            RedirectUrl = dto.RedirectUrl,
            CategoryId = dto.CategoryId,
            ImageUrl = imagePath
        };

        await _repo.CreateAsync(banner);
        return Ok(banner);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] BannerUpdateDto dto)
    {
        var banner = await _repo.GetByIdAsync(id);
        if (banner == null) return NotFound();

        banner.Title = dto.Title;
        banner.BannerType = dto.BannerType;
        banner.DisplayOrder = dto.DisplayOrder;
        banner.IsActive = dto.IsActive;
        banner.RedirectUrl = dto.RedirectUrl;
        banner.CategoryId = dto.CategoryId;

        // Replace image if a new one was uploaded
        if (dto.ImageFile != null)
        {
            string newImagePath = ImageHelper.SaveBannerImage(dto.ImageFile, _env.WebRootPath);
            banner.ImageUrl = newImagePath;
        }

        await _repo.UpdateAsync(banner);

        return Ok(banner);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var banner = await _repo.GetByIdAsync(id);
        if (banner == null) return NotFound();

        // Delete image file
        var fullPath = _env.WebRootPath + banner.ImageUrl;
        if (System.IO.File.Exists(fullPath))
            System.IO.File.Delete(fullPath);

        await _repo.DeleteAsync(banner);
        return Ok();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadBanner([FromForm] BannerUploadDto dto)
    {
        using var ms = new MemoryStream();
        await dto.Image.CopyToAsync(ms);

        var appDto = new BannerUpdateDto
        {
            Id = dto.Id,
            ImageBytes = ms.ToArray(),
            FileName = dto.Image.FileName
        };

        await _bannerService.UpdateAsync(appDto);

        return Ok();
    }

}
