using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;
using WebApiRestful.ViewModel;

namespace WebApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        UserManager<ApplicationUser> _userManager;
        PasswordHasher<ApplicationUser> _passwordHasher;
        PasswordValidator<ApplicationUser> _passwordValidator;

        public UserController(IMapper mapper, 
                                UserManager<ApplicationUser> userManager, 
                                PasswordHasher<ApplicationUser> passwordHasher,
                                PasswordValidator<ApplicationUser> passwordValidator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }


        [HttpPost] 
        public async Task<IActionResult> Register([FromBody] UserModel userVM) {

            if(userVM is null) {
                return BadRequest("Invalid Data");
            }

            var user = _mapper.Map<ApplicationUser>(userVM);

            //user.EmailConfirmed = true;

            var validationPassword = await _passwordValidator.ValidateAsync(_userManager, user, userVM.Password);

            if (!validationPassword.Succeeded) {
                return BadRequest(validationPassword.Errors);
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                return Ok(true);
            }
            else
                return BadRequest(result.Errors);
        }


        [HttpGet]
        public IActionResult Update()
        {
            //UserModel userModel = new()
            //{
            //    Id = 0,
            //    Fullname = "Robot",
            //    Password = "123",
            //    Username = "robot.123"
            //};

            //User user = _mapper.Map<User>(userModel);


            //UserModel modelUser = _mapper.Map<UserModel>(user);

            //List<User> users = new List<User>() { user };

            //var ls = _mapper.Map<List<UserModel>>(users);


            return Ok(1);
        }
    }
}
