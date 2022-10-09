using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.WebApiRestful.Domain.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Required]
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
