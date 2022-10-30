using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiRestful.Service.Abstract;
using WebApiRestful.ViewModel;

namespace WebApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
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

            //


            return Ok(true);
        }

    }
}
