using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Restful.Core.EmailHelper;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Infrastructure.CommonService;
using WebApiRestful.ViewModel;

namespace WebApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;
        private readonly PasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IEmailHelper _emailHelper;
        private readonly IEmailTemplateReader _emailTemplateReader;

        public UserController(IMapper mapper, 
                                UserManager<ApplicationUser> userManager, 
                                PasswordHasher<ApplicationUser> passwordHasher,
                                PasswordValidator<ApplicationUser> passwordValidator,
                                IEmailHelper  emailHelper,
                                IEmailTemplateReader emailTemplateReader)
        {
            _mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _emailHelper = emailHelper;
            _emailTemplateReader = emailTemplateReader;
        }


        [HttpPost] 
        public async Task<IActionResult> Register(CancellationToken cancellationToken,  [FromBody] UserModel userVM) {

            if(userVM is null) {
                return BadRequest("Invalid Data");
            }

            var user = _mapper.Map<ApplicationUser>(userVM);

            //user.EmailConfirmed = true;

            //var validationPassword = await _passwordValidator.ValidateAsync(_userManager, user, userVM.Password);

            //if (!validationPassword.Succeeded) {
            //    return BadRequest(validationPassword.Errors);
            //}

            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                //Send Email for Confirm
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string url = Url.Action("ConfirmEmail", "User", new { userId = user.Id, token }, Request.Scheme);

                string body = await _emailTemplateReader.GetTemplate("Templates\\ConfirmEmail.html");
                body = string.Format(body, user.Fullname, url);

                await _emailHelper.SendEmailAsync(cancellationToken, new Domain.Model.EmailRequest
                {
                    To = user.Email,
                    Subject = "Confirm Email For Register",
                    Content = body
                });


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

        [HttpGet("confirm-email")]
        public IActionResult ConfirmEmail()
        {


            return Ok(1);
        }
    }
}
