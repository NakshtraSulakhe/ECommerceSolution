using ECommerce.Api.DTOs;
using ECommerce.API.Helpers;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _productRepository.GetAllAsync());

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _productRepository.UpdateAsync(product);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productRepository.DeleteAsync(product);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(
            [FromForm] ProductImageUploadDto dto,
            [FromServices] ImageHelper helper,
            [FromServices] ICategoryRepository categoryRepo)
        {
            if (dto.Image == null || dto.Image.Length == 0)
                return BadRequest("Image File is required.");

            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product == null)
                return NotFound("Product Not Found.");

            var category = await categoryRepo.GetByIdAsync(product.CategoryId);
            if (category == null)
                return BadRequest("Invalid category assigned to product.");

            var imageUrl = await helper.SaveProductImageAsync(dto.Image, category.Slug);

            product.ImageUrl = imageUrl;
            await _productRepository.UpdateAsync(product);

            return Ok(new
            {
                productId = product.Id,
                category = category.Slug,
                imageUrl = product.ImageUrl
            });
        }
    }
}
