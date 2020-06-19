using Property.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Property.Domain.DTOs
{
    public class PropertyBaseDto
    {
        [Required]
        [MaxLength(30)]
        public string PropertyName { get; set; }
        [Required]
        public int PropertyNumber { get; set; }
        [Required]
        public double Area { get; set; }
        public Status PropertyStatus { get; set; }
        public string Address { get; set; }
    }
}
