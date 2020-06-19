using Property.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Property.Provider
{
    public interface IPropertyProvider
    {
        Task<IEnumerable<PropertyDto>> GetAllProperties(int landlordId);
        Task<PropertyDto> GetProperty(int landlordId, int id);
        PropertyDto AddProperty(int landlordId, PropertyBaseDto property);
        bool? EditProperty(PropertyDto property);

        Task<bool?> DeleteProperty(int id);
    }
}
