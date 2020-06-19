using System.ComponentModel.DataAnnotations;

namespace Property.Domain.DTOs
{
    public class LandlordBaseDto
    {
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
    }
}
