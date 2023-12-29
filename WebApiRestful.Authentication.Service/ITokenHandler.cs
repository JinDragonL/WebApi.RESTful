using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Domain.Model;

namespace WebApiRestful.Authentication.Service
{
    public interface ITokenHandler
    {
        /// <summary>
        /// Validate token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ValidateToken(TokenValidatedContext context);
        Task<string> CreateAccessToken(ApplicationUser user);
        Task<string> CreateRefreshToken(ApplicationUser user);
        Task<JwtModel> ValidateRefreshToken(string refreshToken);
    }
}