using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRestful.Domain.Entities
{
    public class DBLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string LoggedDate { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
    }

}
