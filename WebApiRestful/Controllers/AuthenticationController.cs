using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiRestful.Authentication.Service;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Domain.Model;
using WebApiRestful.Service.Abstract;
using WebApiRestful.ViewModel;

namespace WebApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IUserService _userService;
        ITokenHandler _tokenHandler;
        IUserTokenService _userTokenService;

        public AuthenticationController(IUserService userService, ITokenHandler tokenHandler, 
                                        IUserTokenService userTokenService)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
            _userTokenService = userTokenService;
        }

        //FluentValidation

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(IValidator<AccountModel> validator, [FromBody] AccountModel accountModel)
        {
            var validations = await validator.ValidateAsync(accountModel);

            if(!validations.IsValid)
            {
                return BadRequest(validations.Errors.Select(x => new ErrorValdations
                {
                    FieldName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                }));
            }

            var user = await _userService.CheckLogin(accountModel.Username, accountModel.Password);

            if(user == null)
            {
                return Unauthorized();
            }

            (string accessToken, DateTime expiredDateAccess) = await _tokenHandler.CreateAccessToken(user);
            (string code, string refreshToken, DateTime expiredDateRefresh) = await _tokenHandler.CreateRefreshToken(user);

            await _userTokenService.SaveToken(new Domain.Entities.UserToken
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                CodeRefreshToken = code,
                ExpiredDateAccessToken = expiredDateAccess,
                ExpiredDateRefreshToken = expiredDateRefresh,
                CreatedDate = DateTime.Now,
                UserId = user.Id,
                IsActive = true
            });

            return Ok(new JwtModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Fullname = user.DisplayName,
                Username = user.Username
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel token)
        {
            if (token == null) 
                return BadRequest("Could not get refresh token");

            return Ok(await _tokenHandler.ValidateRefreshToken(token.RefreshToken));
        }
    }
}
