using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.ViewModel
{
    public class AccountModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        //DTO
    }
}
