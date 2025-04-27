using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
   public interface IUserService
    {
        public Task<User> Login(UserLoginModel model);
        public Task<bool> Register(UserRegisterModel model);
        public  void DeleteUser(int id);
        public User GetUserByEmail(string email);
        public Task<bool> UpdateUser(int userId, UserRegisterModel model);
        public string ForgetPassword(string email);
        public bool ResetPassword(string Email, ResetPasswordModel model);
       
    }

}
