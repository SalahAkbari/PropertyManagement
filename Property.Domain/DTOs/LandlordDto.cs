using System.Collections.Generic;

namespace Property.Domain.DTOs
{
    public class LandlordDto: LandlordBaseDto
    {
        public int LandlordId { get; set; }
        public ICollection<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
    }
}
