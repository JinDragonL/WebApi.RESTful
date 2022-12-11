using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.ViewModel
{
    public class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
