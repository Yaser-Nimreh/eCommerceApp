using eCommerceApp.Application.DTOs.Catalog.Category;
using eCommerceApp.Application.Services.Interfaces.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await categoryService.GetAllAsync();
            return result.Any() ? Ok(result) : NotFound(result);
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await categoryService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound(result);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(CreateCategory category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryService.CreateAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(UpdateCategory category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryService.UpdateAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await categoryService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("products-by-category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryAsync(Guid categoryId)
        {
            var result = await categoryService.GetProductsByCategoryAsync(categoryId);
            return result.Any() ? Ok(result) : NotFound();
        }
    }
}