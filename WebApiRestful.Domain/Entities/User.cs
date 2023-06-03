using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.Domain.Entities
{
    //[Table("Account")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }

        public string DisplayName { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
