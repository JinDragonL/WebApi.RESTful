using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
