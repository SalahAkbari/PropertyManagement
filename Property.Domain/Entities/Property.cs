using Property.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Property.Domain.Entities
{
    public class Property: IIdentifier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropertyID { get; set; }
        [Required]
        [MaxLength(30)]
        public string PropertyName { get; set; }
        [Required]
        public int PropertyNumber { get; set; }
        [Required]
        public double Area { get; set; }
        public Status PropertyStatus { get; set; }
        public string Address { get; set; }

        [ForeignKey("LandlordId")]
        public int LandlordId { get; set; }
        public Landlord Landlord { get; set; }
    }
}
