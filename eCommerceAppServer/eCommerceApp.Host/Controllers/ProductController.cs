using eCommerceApp.Application.DTOs.Catalog.Product;
using eCommerceApp.Application.Services.Interfaces.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await productService.GetAllAsync();
            return result.Any() ? Ok(result) : NotFound(result);
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await productService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound(result);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(CreateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await productService.CreateAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(UpdateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await productService.UpdateAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await productService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}