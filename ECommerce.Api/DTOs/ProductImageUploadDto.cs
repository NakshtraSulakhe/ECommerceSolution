using Microsoft.AspNetCore.Http;

namespace ECommerce.Api.DTOs
{
    public class ProductImageUploadDto
    {
        public int ProductId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
