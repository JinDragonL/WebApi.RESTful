using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Service.Abstract
{
    public interface IUserService
    {
        Task<ApplicationUser> CheckLogin(string username, string password);
    }
}