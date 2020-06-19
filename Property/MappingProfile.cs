using AutoMapper;
using Property.Domain.DTOs;
using Property.Domain.Entities;

namespace Property
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Property, PropertyBaseDto>();
            CreateMap<PropertyBaseDto, Domain.Entities.Property>();
            CreateMap<Domain.Entities.Property, PropertyDto>();
            CreateMap<PropertyDto, Domain.Entities.Property>();

            CreateMap<Landlord, LandlordBaseDto>();
            CreateMap<LandlordBaseDto, Landlord>();
            CreateMap<Landlord, LandlordDto>();
            CreateMap<LandlordDto, Landlord>();
        }
    }
}
