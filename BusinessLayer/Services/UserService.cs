using BusinessLayer.Interfaces;
using ModelLayer;
using RepoLayer.Entity;
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
        private readonly IAuthService _authService;
        public UserService(IUserRepoServices UserRepoServices, IAuthService authService)
        {
            _userRepoServices = UserRepoServices;
            _authService = authService;
        }
        public Task<User> Login(UserLoginModel model)
        {
            var user = _userRepoServices.Login(model);
            return user;
        }
        public Task<bool> Register(UserRegisterModel model)
        {
            var success = _userRepoServices.AddUser(model);
            return success;
        }
        public User GetUserByEmail(string email)
        {
            var user = _userRepoServices.GetUserByEmail(email);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return user;
        }
       
        public async Task<bool> UpdateUser(int userId, UserRegisterModel model)
        {
          return await _userRepoServices.UpdateUser(userId, model);
          
        }

        public string ForgetPassword(string email)
        {
            User user = _userRepoServices.GetUserByEmail(email);
            if (user == null)
                throw new Exception("User does not exist");

            int userId = user.userId;
            string newToken = _authService.GenerateToken(user);
            return _userRepoServices.ForgetPassword(newToken, email);
        }

        public bool ResetPassword(string Email, ResetPasswordModel model)
        {
            return _userRepoServices.ResetPassword(Email, model);
        }

        public void DeleteUser(int id)
        {
             _userRepoServices.DeleteUser(id);
        }
        public bool UpdateUserToken(User user)
        {
            return _userRepoServices.UpdateUserToken(user);
        }
    }
}
