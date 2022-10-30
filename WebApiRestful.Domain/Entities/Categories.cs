using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.Domain.Entities
{
    public class Categories: BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string  Description { get; set; }
    }
}
