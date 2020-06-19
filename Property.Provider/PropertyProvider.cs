using AutoMapper;
using Property.DataAccess;
using Property.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Property.Provider
{
    public class PropertyProvider : IPropertyProvider
    {
        readonly IGenericEfRepository<Domain.Entities.Property> _rep;
        private readonly IMapper _mapper;
        public PropertyProvider(IGenericEfRepository<Domain.Entities.Property> rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }
        public PropertyDto AddProperty(int landlordId, PropertyBaseDto property)
        {
            try
            {
                var itemToCreate = _mapper.Map<Domain.Entities.Property>(property);
                itemToCreate.LandlordId = landlordId;
                _rep.Add(itemToCreate);
                _rep.Save();
                var createdDto = _mapper.Map<PropertyDto>(itemToCreate);
                return createdDto;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public bool? EditProperty(PropertyDto property)
        {
            try
            {
                if (!_rep.Exists(property.PropertyId)) return false;
                var entityToUpdate = _mapper.Map<Domain.Entities.Property>(property);
                _rep.Edit(entityToUpdate, entityToUpdate.PropertyID);
                if (!_rep.Save()) return null;
                return true;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<bool?> DeleteProperty(int id)
        {
            try
            {
                if (!_rep.Exists(id)) return false;
                var entityToDelete = await _rep.Get(id);
                _rep.Delete(entityToDelete);
                if (!_rep.Save()) return null;
                return true;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }

        public async Task<IEnumerable<PropertyDto>> GetAllProperties(int landlordId)
        {
            try
            {
                var item = (await _rep.Get()).Where(b => b.LandlordId.Equals(landlordId));
                var dtOs = _mapper.Map<IEnumerable<PropertyDto>>(item);
                return dtOs;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<PropertyDto> GetProperty(int landlordId, int id)
        {
            try
            {
                var item = await _rep.Get(id);
                if (item == null || !item.LandlordId.Equals(landlordId)) return null;
                var dto = _mapper.Map<PropertyDto>(item);
                return dto;

            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }
    }
}
