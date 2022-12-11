using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApiRestful.Authentication.Service;
using WebApiRestful.Domain.Model;
using WebApiRestful.Service;
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

        public AuthenticationController(IUserService userService, ITokenHandler tokenHandler, IUserTokenService userTokenService)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
            _userTokenService = userTokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccountModel accountModel)
        {
            if(accountModel == null)
            {
                return BadRequest("User is not exist");
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
                Username = user.Username,
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel token)
        {
            return Ok(await _tokenHandler.ValidateRefreshToken(token));
        }
    }
}
