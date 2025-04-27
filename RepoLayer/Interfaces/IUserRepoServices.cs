using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interfaces
{
    public interface IUserRepoServices
    {
        Task<User> Login(UserLoginModel model);
        Task<bool> AddUser(UserRegisterModel model);
        public User GetUserByEmail(string email);
        public Task<bool> UpdateUser(int userId, UserRegisterModel model);
        public void DeleteUser(int id);
        public string ForgetPassword(string newToken, string email);
        public bool ResetPassword(string Email, ResetPasswordModel model);
        
    }
}
