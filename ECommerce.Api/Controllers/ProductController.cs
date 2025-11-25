using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        public readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _productRepository.GetAllAsync());

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]

        public async Task<IActionResult> Create (Product product)
        {
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.id)
                return BadRequest();

            await _productRepository.UpdateAsync(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete (int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            await _productRepository.DeleteAsync(product);
            return NoContent();
        }
    }
}
