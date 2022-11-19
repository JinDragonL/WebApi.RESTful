using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Authentication.Service
{
    public interface ITokenHandler
    {
        Task<string> CreateToken(User user);
        Task ValidateToken(TokenValidatedContext context);
    }
}