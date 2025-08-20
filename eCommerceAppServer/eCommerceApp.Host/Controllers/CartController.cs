using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpPost("checkout")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CheckoutAsync(Checkout checkout)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await cartService.CheckoutAsync(checkout);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("save-checkout")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SaveCheckoutHistoryAsync(IEnumerable<CreateAchieve> achieves)
        {
            var result = await cartService.SaveCheckoutHistoryAsync(achieves);
            return result.Success ? BadRequest(result) : BadRequest(result);
        }

        [HttpGet("get-achieves")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAchievesAsync()
        {
            var achieves = await cartService.GetAchievesAsync();
            return achieves.Any() ? Ok(achieves) : NotFound();
        }
    }
}