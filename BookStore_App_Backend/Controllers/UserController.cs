using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System.Security.Claims;

namespace BookStore_App_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public userController(IUserService userService, IAuthService authService, IEmailService emailService)
        {
            _userService = userService;
            _authService = authService;
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Validation Failed",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var result = await _userService.Register(model);
            if (!result)
            {
                return BadRequest(new { message = "User registration failed" });
            }
            return Ok(new { success = true, message = "User Registered Successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation Failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var user = await _userService.Login(model);
                if (user == null)
                {

                    return BadRequest(new { message = "Invalid credentials" });
                }
                var token = _authService.GenerateToken(user);
                var refreshToken = _authService.GenerateRefreshToken();
                user.refreshToken = refreshToken;
                user.refreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _userService.UpdateUserToken(user);

                return Ok(new { success = true, message = "User Login Succssfully", data = new { token, refreshToken } });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = "Invalid credentials" });
            }
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPassModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation Failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }
                string resetLink = _userService.ForgetPassword(model.Email);
                await _emailService.SendEmailAsync(model.Email, resetLink);
                return Ok(new ResponseModel<string> { success = true, message = "Reset link sent to your email", data = resetLink });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { success = false, message = e.Message });
            }
        }
        [Authorize(Roles = "User")]
        [HttpPost("set-newpassword")]
        public IActionResult SetNewPassword([FromBody] ResetPasswordModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation Failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var email = User.FindFirstValue(ClaimTypes.Email);
                Console.WriteLine(email);

                bool isReset = _userService.ResetPassword(email, model);
                if (!isReset)
                {
                    return BadRequest(new { success = false, message = "Failed to reset password." });
                }

                return Ok(new { success = true, message = "Password reset successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred", Error = ex.Message });
            }
        }
    }
}
