using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Property.Domain.Entities
{
    public class Landlord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LandlordId { get; set; }

        [Required]
        [MaxLength(30)]
        public string LandlordName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LandlordFamily { get; set; }

        [Required(ErrorMessage = "You must provide a Mobile number")]
        [Display(Name = "Mobile No")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MobileNo { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
