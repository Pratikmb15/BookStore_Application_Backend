using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStore_App_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("refresh-token")]
        public IActionResult Refresh([FromBody] TokenRequestModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest(new { success = false, message = "Invalid client request" });
            }

            var principal = _authService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);

            if (principal == null)
            {
                return BadRequest(new{success = false, message = "Invalid access token or refresh token" });
               
            }

            

            var newAccessToken = _authService.GenerateTokenFromPrincipal(principal);
            var newRefreshToken = _authService.GenerateRefreshTokenFromPrincipal(principal);

            return Ok(new { success=true,message="Refreshed Tokens successfully", data = new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            } });
        }
    }
}
