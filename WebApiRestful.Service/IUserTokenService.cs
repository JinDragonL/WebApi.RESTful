using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Service
{
    public interface IUserTokenService
    {
        Task<UserToken> CheckRefreshToken(string code);
        Task SaveToken(UserToken userToken);
    }
}