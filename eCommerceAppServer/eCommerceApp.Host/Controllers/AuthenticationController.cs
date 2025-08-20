using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUserAsync(CreateUser user)
        {
            var result = await authenticationService.CreateUserAsync(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUserAsync(LoginUser user)
        {
            var result = await authenticationService.LoginUserAsync(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("refresh-token/{refreshToken}")]
        public async Task<IActionResult> RefreshTokenAsync(string refreshToken)
        {
            var result = await authenticationService.RefreshTokenAsync(HttpUtility.UrlDecode(refreshToken));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}