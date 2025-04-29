using BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // <-- ignore token expiry here
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        //    public string GenerateRefreshToken(User user)
        //    {
        //        var claims = new[]
        //        {
        //    new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
        //    new Claim(ClaimTypes.Role, user.role),
        //    new Claim(ClaimTypes.Name, user.fullName),
        //    new Claim(ClaimTypes.Email, user.email)
        //};

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var refreshToken = new JwtSecurityToken(
        //            issuer: _config["Jwt:Issuer"],
        //            audience: _config["Jwt:Audience"],
        //            claims: claims,
        //            expires: DateTime.Now.AddDays(7),  // refresh token expiry is longer
        //            signingCredentials: creds);

        //        return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        //    }
        //    public string GenerateRefreshToken(Admin admin)
        //    {
        //        var claims = new[]
        //        {
        //    new Claim(ClaimTypes.NameIdentifier, admin.userId.ToString()),
        //    new Claim(ClaimTypes.Role, admin.role),
        //    new Claim(ClaimTypes.Name, admin.fullName),
        //    new Claim(ClaimTypes.Email, admin.email)
        //};

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var refreshToken = new JwtSecurityToken(
        //            issuer: _config["Jwt:Issuer"],
        //            audience: _config["Jwt:Audience"],
        //            claims: claims,
        //            expires: DateTime.Now.AddDays(7),  // refresh token expiry is longer
        //            signingCredentials: creds);

        //        return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        //    }

        //    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        //    {
        //        var tokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidateLifetime = false, 
        //            ValidIssuer = _config["Jwt:Issuer"],
        //            ValidAudience = _config["Jwt:Audience"],
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
        //        };

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        //        return principal;
        //    }

        //    public string GenerateTokenFromPrincipal(ClaimsPrincipal principal)
        //    {
        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var token = new JwtSecurityToken(
        //            issuer: _config["Jwt:Issuer"],
        //            audience: _config["Jwt:Audience"],
        //            claims: principal.Claims,
        //            expires: DateTime.Now.AddHours(1),  // access token short expiry
        //            signingCredentials: creds);

        //        return new JwtSecurityTokenHandler().WriteToken(token);
        //    }

        //    public string GenerateRefreshTokenFromPrincipal(ClaimsPrincipal principal)
        //    {
        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var token = new JwtSecurityToken(
        //            issuer: _config["Jwt:Issuer"],
        //            audience: _config["Jwt:Audience"],
        //            claims: principal.Claims,
        //            expires: DateTime.Now.AddDays(7), 
        //            signingCredentials: creds);

        //        return new JwtSecurityTokenHandler().WriteToken(token);
        //    }


    }
}
