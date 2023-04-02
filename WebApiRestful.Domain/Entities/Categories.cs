using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.Domain.Entities
{
    public class Categories: BaseEntity
    {
        [StringLength(300)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
