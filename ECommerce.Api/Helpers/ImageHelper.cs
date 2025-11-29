using Microsoft.AspNetCore.Hosting;

namespace ECommerce.API.Helpers;

public class ImageHelper
{
    private readonly IWebHostEnvironment _env;

    public ImageHelper(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveProductImageAsync(IFormFile file, string categorySlug)
    {
        // category-wise folder
        var folderPath = Path.Combine(_env.WebRootPath, "images", "products", categorySlug);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Final Image URL
        return $"/images/products/{categorySlug}/{fileName}";
    }

    public async Task<string> SaveBannerImageAsync(IFormFile file, string bannerType)
    {
        var folderPath = Path.Combine(_env.WebRootPath, "images", "banners", bannerType);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/images/banners/{bannerType}/{fileName}";
    }

}
