using BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config) => _config = config;

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
            new Claim(ClaimTypes.Role, user.role),
            new Claim(ClaimTypes.Name, user.fullName),
            new Claim(ClaimTypes.Email, user.email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateToken(Admin admin)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, admin.userId.ToString()),
            new Claim(ClaimTypes.Role, admin.role),
            new Claim(ClaimTypes.Name, admin.fullName),
            new Claim(ClaimTypes.Email, admin.email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token, out string email)
        {
            email = null;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // No delay in expiration
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                return email != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string GenerateRefreshToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
        new Claim(ClaimTypes.Role, user.role),
        new Claim(ClaimTypes.Name, user.fullName),
        new Claim(ClaimTypes.Email, user.email)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var refreshToken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),  // refresh token expiry is longer
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }
        public string GenerateRefreshToken(Admin admin)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, admin.userId.ToString()),
        new Claim(ClaimTypes.Role, admin.role),
        new Claim(ClaimTypes.Name, admin.fullName),
        new Claim(ClaimTypes.Email, admin.email)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var refreshToken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),  // refresh token expiry is longer
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // Ignore expiry
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            return principal;
        }

        public string GenerateTokenFromPrincipal(ClaimsPrincipal principal)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: principal.Claims,
                expires: DateTime.Now.AddHours(1),  // access token short expiry
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshTokenFromPrincipal(ClaimsPrincipal principal)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: principal.Claims,
                expires: DateTime.Now.AddDays(7), // refresh token long expiry
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
