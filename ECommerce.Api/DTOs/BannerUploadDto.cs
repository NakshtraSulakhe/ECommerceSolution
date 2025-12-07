using Microsoft.AspNetCore.Http;

namespace ECommerce.Api.DTOs
{
    public class BannerUploadDto
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
    }
}
