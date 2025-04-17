using BusinessLayer.Interfaces;
using ModelLayer;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepoServices _userRepoServices;
        public UserService(IUserRepoServices UserRepoServices)
        {
            _userRepoServices = UserRepoServices;
        }
        public Task<bool> Login(UserLoginModel model)
        {
            var success = _userRepoServices.Login(model);
            return success;
        }

        public Task<bool> Register(UserRegisterModel model)
        {
           var success = _userRepoServices.AddUser(model);
            return success;
        }
    }
}
