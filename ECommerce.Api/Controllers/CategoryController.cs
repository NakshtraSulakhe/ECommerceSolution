using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repository.GetAllAsync());

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        [AllowAnonymous]
        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var category = await _repository.GetBySlugAsync(slug);
            return category == null ? NotFound() : Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<IActionResult> Create(Category category)
        {
            await _repository.AddAsync(category);
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();
            await _repository.UpdateAsync(category);
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();

            await _repository.DeleteAsync(category);
            return Ok();

        }
    }
}
