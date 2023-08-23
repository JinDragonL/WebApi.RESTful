using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
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

        public async Task<ApplicationUser> CheckLogin(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return default(ApplicationUser);
            }

            var isExist = await _userManager.CheckPasswordAsync(user, password);

            if(!isExist)
            {
                return default(ApplicationUser);
            }

            return user;
        }
    }
}
