using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(250)]
        public string Fullname { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
    }
}
