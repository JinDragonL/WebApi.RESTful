using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Domain.Model;

namespace WebApiRestful.Authentication.Service
{
    public interface ITokenHandler
    {
        Task ValidateToken(TokenValidatedContext context);
        Task<(string, DateTime)> CreateAccessToken(User user);
        Task<(string, string, DateTime)> CreateRefreshToken(User user);
        Task<JwtModel> ValidateRefreshToken(string refreshToken);
    }
}