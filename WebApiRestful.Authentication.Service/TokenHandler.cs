using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Domain.Model;

namespace WebApiRestful.Authentication.Service
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;
        UserManager<ApplicationUser> _userManager;

        private const string TokenBearIssuer = "TokenBear:Issuer";
        private const string TokenBearAudience = "TokenBear:Audience";
        private const string TokenBearSignatureKey = "TokenBear:SignatureKey";
        private const string TokenBearExpiredTimeByMinutes = "TokenBear:AccessTokenExpiredByMinutes";
        private const string AccessTokenProvider = "AccessTokenProvider";
        private const string RefreshTokenProvider = "RefreshTokenProvider";
        private const string AccessToken = "AccessToken";
        private const string RefreshToken = "RefreshToke";

        public TokenHandler(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateAccessToken(ApplicationUser user)
        {
            string issuer = _configuration[TokenBearIssuer];
            string audience = _configuration[TokenBearAudience];
            string signatureKey = _configuration[TokenBearSignatureKey];
            int tokenExpirationMinutes = int.Parse(_configuration[TokenBearExpiredTimeByMinutes]);

            DateTime expiresAt = DateTime.Now.AddMinutes(tokenExpirationMinutes);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, audience, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, issuer)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = expiresAt,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

            await _userManager.SetAuthenticationTokenAsync(user, AccessTokenProvider, AccessToken, tokenHandler.WriteToken(accessToken));

            return tokenHandler.WriteToken(accessToken);
        }

        public async Task<string> CreateRefreshToken(ApplicationUser user)
        {
            string issuer = _configuration[TokenBearIssuer];
            string audience = _configuration[TokenBearAudience];
            string signatureKey = _configuration[TokenBearSignatureKey];
            int refreshTokenExpirationHours = int.Parse(_configuration["TokenBear:RefreshTokenExpiredByHours"]);

            DateTime expiresAt = DateTime.Now.AddHours(refreshTokenExpirationHours);

            string refreshTokenCode = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, issuer),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.SerialNumber, refreshTokenCode, ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, issuer)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = expiresAt,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var refreshToken = tokenHandler.CreateToken(tokenDescriptor);

            await _userManager.SetAuthenticationTokenAsync(user, RefreshTokenProvider, RefreshToken, tokenHandler.WriteToken(refreshToken));

            return tokenHandler.WriteToken(refreshToken);
        }

        public async Task ValidateToken(TokenValidatedContext context)
        {
            var claims = context.Principal.Claims.ToList();

            if (claims.Count == 0)
            {
                context.Fail("This token contains no information");
                return;
            }

            var identity = context.Principal.Identity as ClaimsIdentity;

            if (identity.FindFirst(JwtRegisteredClaimNames.Iss) == null)
            {
                context.Fail("This token is not issued by point entry");
                return;
            }

            if (identity.FindFirst("Username") != null)
            {
                string username = identity.FindFirst("Username").Value;

                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    context.Fail("This token is invalid for user");
                    return;
                }
            }

            //if (identity.FindFirst(JwtRegisteredClaimNames.Exp) != null)
            //{
            //    var dateExp = identity.FindFirst(JwtRegisteredClaimNames.Exp).Value;

            //    long ticks = long.Parse(dateExp);
            //    var date = DateTimeOffset.FromUnixTimeSeconds(ticks).DateTime;

            //    var minutes = date.Subtract(DateTime.Now).TotalMinutes;

            //    //if(minutes < 0)
            //    //{
            //    //    context.Fail("This token is expired.");

            //    //    throw new Exception("This token is expired.");
            //    //    return;
            //    //}

            //}
        }

        public async Task<JwtModel> ValidateRefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, GetTokenValidationParameters(), out _);

                string username = principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

                var user = await _userManager.FindByNameAsync(username);

                if (user != null)
                {
                    var existingAccessToken = await _userManager.GetAuthenticationTokenAsync(user, AccessTokenProvider, AccessToken);

                    if (!string.IsNullOrEmpty(existingAccessToken))
                    {
                        string newAccessToken = await CreateAccessToken(user);
                        string newRefreshToken = await CreateRefreshToken(user);

                        return new JwtModel
                        {
                            AccessToken = newAccessToken,
                            RefreshToken = newRefreshToken
                        };
                    }
                }
            }
            catch (SecurityTokenException)
            {
                // Token validation failed
            }

            return new JwtModel();
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[TokenBearSignatureKey])),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

    }
}
