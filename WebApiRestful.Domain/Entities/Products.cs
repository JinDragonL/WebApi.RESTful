using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.WebApiRestful.Domain.Entities
{
    public class Products: BaseEntity
    {
        [StringLength(1000)]
        public string Name { get; set; }

        public int QuatityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UnitOnOrder { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories Categories { get; set; }
    }
}
