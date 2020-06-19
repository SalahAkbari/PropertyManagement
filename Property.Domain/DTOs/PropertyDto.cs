namespace Property.Domain.DTOs
{
    public class PropertyDto : PropertyBaseDto
    {
        public int PropertyId { get; set; }
        public int LandlordId { get; set; }
    }
}
