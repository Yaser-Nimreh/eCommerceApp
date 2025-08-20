using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentMethodService paymentMethodService) : ControllerBase
    {
        [HttpGet("methods")]
        public async Task<IActionResult> GetPaymentMethodsAsync()
        {
            var result = await paymentMethodService.GetPaymentMethodsAsync();
            if (!result.Any()) return NotFound();
            return Ok(result);
        }
    }
}