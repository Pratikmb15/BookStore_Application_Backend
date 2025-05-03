using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepoLayer.Context;
using System.Security.Claims;

namespace BookStore_App_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;
        private readonly AppDbContext _context;

        public AuthController(IAuthService authService, IUserService userService, AppDbContext context,IAdminService adminService)
        {
            _authService = authService;
            _userService = userService;
            _context = context;
            _adminService = adminService;
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] TokenRequestModel tokenModel)
        {
            try
            {
                if (tokenModel is null)
                {
                    return BadRequest(new { success = false, message = "Invalid client request" });
                }

                var principal = _authService.GetPrincipalFromExpiredToken(tokenModel.accessToken);
                var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                Console.WriteLine(email);

                var user = _context.Users.FirstOrDefault(u => u.email == email);
                if (user == null || user.refreshToken != tokenModel.refreshToken || user.refreshTokenExpiryTime <= DateTime.Now)
                {
                    var admin = _context.Admins.FirstOrDefault(a => a.email == email);

                    if (admin == null || admin.refreshToken != tokenModel.refreshToken || admin.refreshTokenExpiryTime <= DateTime.Now)
                    {
                        return BadRequest(new { success = false, message = "Invalid refresh token" });
                    }

                    var newAccessToken = _authService.GenerateToken(admin);
                    var newRefreshToken = _authService.GenerateRefreshToken();

                    admin.refreshToken = newRefreshToken;
                    admin.refreshTokenExpiryTime = DateTime.Now.AddDays(7);
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Refreshed Tokens successfully",
                        data = new
                        {
                            accessToken = newAccessToken,
                            refreshToken = newRefreshToken
                        }
                    });
                }

                var newAccessTokenUser = _authService.GenerateToken(user);
                var newRefreshTokenUser = _authService.GenerateRefreshToken();

                user.refreshToken = newRefreshTokenUser;
                user.refreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Refreshed Tokens successfully",
                    data = new
                    {
                        accessToken = newAccessTokenUser,
                        refreshToken = newRefreshTokenUser
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

    }
}
