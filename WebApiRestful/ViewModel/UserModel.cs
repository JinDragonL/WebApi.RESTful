using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.ViewModel
{
    public class UserModel: AccountModel
    {
        public string Fullname { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
