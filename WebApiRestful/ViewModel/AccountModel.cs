using FluentValidation;
using System.Threading.Tasks;
using System.Threading;
using WebApiRestful.Service.Abstract;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.ViewModel
{
    public class AccountModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
    }

    public class LoginValidator : AbstractValidator<AccountModel>
    {
        IUserService _userService;

        public LoginValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(m =>  m.Username)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("{PropertyName} is invalid")
                .MustAsync(Exist)
                .WithMessage("{PropertyName} is exist, please choose another");

            RuleFor(m =>  m.Password)
                .NotEmpty()
                .MinimumLength(3)
                .MinimumLength(20)
                .WithMessage("Password is greater 3 and less than 20")
                .Must((password) =>
                {
                    if(password.Length < 3) { return false;  }

                    return true;
                });
        }

       
        private async Task<bool> Exist(string username, CancellationToken cancellationToken)
        {
            ApplicationUser user = null; // _userService.FindByUsername(username);

            if (user == null) { return true;  }

            return false;
        }
    }
}
