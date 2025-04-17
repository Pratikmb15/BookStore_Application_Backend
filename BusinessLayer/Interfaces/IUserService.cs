using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
   public interface IUserService
    {
        Task<bool> Login(UserLoginModel model);
        Task<bool> Register(UserRegisterModel model);
    }
}
