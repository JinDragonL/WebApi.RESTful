using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Domain.Model;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Authentication.Service
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;
        IUserService _userService;
        IUserTokenService _userTokenService;
        UserManager<ApplicationUser> _userManager;


        public TokenHandler(IConfiguration configuration, IUserService userService, IUserTokenService userTokenService, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userService = userService;
            _userTokenService = userTokenService;
            _userManager = userManager;
        }

        public async Task<string> CreateAccessToken(ApplicationUser user)
        {
            DateTime expiredToken = DateTime.Now.AddMinutes(int.Parse(_configuration["TokenBear:AccessTokenExpiredByMinutes"]));

            var roles = await _userManager.GetRolesAsync(user);

            var cliams = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenBear:Issuer"], ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, "WebApiRestful - .NetChannel", ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, expiredToken.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(ClaimTypes.Name, user.Fullname, ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim("Username", user.UserName, ClaimValueTypes.String, _configuration["TokenBear:Issuer"])
            }
            .Union(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenInfo = new JwtSecurityToken(
                issuer: _configuration["TokenBear:Issuer"],
                audience: _configuration["TokenBear:Audience"],
                claims: cliams,
                notBefore: DateTime.Now,
                expires: expiredToken,
                credential
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            await _userManager.SetAuthenticationTokenAsync(user, "AccessTokenProvider", "AccessToken", accessToken);

            return accessToken;
        }

        public async Task<(string, string)> CreateRefreshToken(ApplicationUser user)
        {
            DateTime expiredToken = DateTime.Now.AddHours(int.Parse(_configuration["TokenBear:RefreshTokenExpiredByHours"]));
            string code = Guid.NewGuid().ToString();

            var cliams = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenBear:Issuer"], ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, expiredToken.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(ClaimTypes.SerialNumber, code, ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim("Username", user.UserName, ClaimValueTypes.String, _configuration["TokenBear:Issuer"])
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenInfo = new JwtSecurityToken(
                issuer: _configuration["TokenBear:Issuer"],
                audience: _configuration["TokenBear:Audience"],
                claims: cliams,
                notBefore: DateTime.Now,
                expires: expiredToken,
                credential
            );

            string refreshToken = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            await _userManager.SetAuthenticationTokenAsync(user, "RefreshTokenProvider", "RefreshToken", refreshToken);

            return await Task.FromResult((code, refreshToken));
        }

        public async Task ValidateToken(TokenValidatedContext context)
        {
            var claims = context.Principal.Claims.ToList();

            if(claims.Count == 0)
            {
                context.Fail("This token contains no information");
                return; 
            }

            var identity = context.Principal.Identity as ClaimsIdentity;

            if(identity.FindFirst(JwtRegisteredClaimNames.Iss) == null)
            {
                context.Fail("This token is not issued by point entry");
                return;
            }

            if(identity.FindFirst("Username") != null)
            {
                string username = identity.FindFirst("Username").Value;

                var user = await _userManager.FindByNameAsync(username);

                if(user == null)
                {
                    context.Fail("This token is invalid for user");
                    return;
                }
            }

            if(identity.FindFirst(JwtRegisteredClaimNames.Exp) != null)
            {
                var dateExp = identity.FindFirst(JwtRegisteredClaimNames.Exp).Value;

                long ticks = long.Parse(dateExp);
                var date = DateTimeOffset.FromUnixTimeSeconds(ticks).DateTime;

                var minutes = date.Subtract(DateTime.Now).TotalMinutes;

                //if(minutes < 0)
                //{
                //    context.Fail("This token is expired.");

                //    throw new Exception("This token is expired.");
                //    return;
                //}
                
            }

            // Record log
            // Update last date
        }

        public async Task<JwtModel> ValidateRefreshToken(string refreshToken)
        {
            var cliamPriciple = new JwtSecurityTokenHandler().ValidateToken(
                    refreshToken,
                    new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"])),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    },
                    out _
                );

            if (cliamPriciple == null) return new();

            string username = cliamPriciple?.Claims?.FirstOrDefault(x => x.Type == "Username")?.Value;

            var user = await _userManager.FindByNameAsync(username);

            var token = await _userManager.GetAuthenticationTokenAsync(user, "AccessTokenProvider", "AccessToken");

            if (!string.IsNullOrEmpty(token))
            {
                string newAccessToken = await CreateAccessToken(user);
                (string codeRefreshToken, string newRefreshToken) = await CreateRefreshToken(user);

                return new JwtModel
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                };
            }

            return new();
        }

    }
}
