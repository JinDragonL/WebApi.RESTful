using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApi.Restful.Core.Common;
using WebApiRestful.Data.Abstract;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Service
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        UserManager<ApplicationUser> _userManager;

        public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> CheckLogin(string username, string encryptedPassword, string key)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return default(ApplicationUser);
            }

            string plainPassword = AESEncryption.DecryptStringAES(encryptedPassword, key);

            var hasExist = await _userManager.CheckPasswordAsync(user, plainPassword);

            if (!hasExist)
            {
                return default(ApplicationUser);
            }

            return user;
        }
    }
}
