using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAuthService
    {
       public string GenerateToken(User user);
       public string GenerateToken(Admin admin);
       public bool ValidateToken(string token, out string email);
    }
}
