using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiRestful.Authentication.Service;
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
            //var validations = await validator.ValidateAsync(accountModel);

            //if(!validations.IsValid)
            //{
            //    return BadRequest(validations.Errors.Select(x => new ErrorValdations
            //    {
            //        FieldName = x.PropertyName,
            //        ErrorMessage = x.ErrorMessage
            //    }));
            //}

            var user = await _userService.CheckLogin(accountModel.Username, accountModel.Password, accountModel.Key);

            if(user == null)
            {
                return Unauthorized();
            }

            if(!user.EmailConfirmed)
            {
                return BadRequest("Your account is inactive");
            }

            string accessToken = await _tokenHandler.CreateAccessToken(user);
            (string code, string refreshToken) = await _tokenHandler.CreateRefreshToken(user);

            return Ok(new JwtModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
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
