using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interfaces
{
    public interface IUserRepoServices
    {
        Task<bool> Login(UserLoginModel model);
        Task<bool> AddUser(UserRegisterModel model);
    }
}
