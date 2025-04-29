using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAuthService
    {
       public string GenerateToken(User user);
       public string GenerateToken(Admin admin);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        //public string GenerateRefreshToken(User user);
        //public string GenerateRefreshToken(Admin admin);
        //public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        //public string GenerateTokenFromPrincipal(ClaimsPrincipal principal);
        //public string GenerateRefreshTokenFromPrincipal(ClaimsPrincipal principal);
    }
}
