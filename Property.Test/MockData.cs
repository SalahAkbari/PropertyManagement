using Property.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Property.Test
{
    public class MockData
    {
        public static MockData Current { get; } = new MockData();
        public List<LandlordDto> Landlords { get; set; }
        public List<Domain.Entities.Property> Properties { get; set; }

        public MockData()
        {
            Landlords = new List<LandlordDto>()
            {
                new LandlordDto() { LandlordId = 1,
                    LandlordName = "Ahmad", LandlordFamily = "Asadi", MobileNo = "0912102030" },
                new LandlordDto() { LandlordId = 2,
                    LandlordName = "Ali", LandlordFamily = "Rahmani", MobileNo = "0912123456" },
                new LandlordDto() { LandlordId = 3,
                    LandlordName = "Sara", LandlordFamily = "Vahidi", MobileNo = "0912112233" }
            };

            Properties = new List<Domain.Entities.Property>
            {
                new Domain.Entities.Property { PropertyName = "prop1" , PropertyNumber = 101, Area = 60, PropertyID = 1, LandlordId = 1, Address = "Address1", PropertyStatus = Domain.Enums.Status.North},
                new Domain.Entities.Property { PropertyName = "prop2" , PropertyNumber = 102, Area = 70, PropertyID = 2, LandlordId = 2, Address = "Address2", PropertyStatus = Domain.Enums.Status.South},
                new Domain.Entities.Property { PropertyName = "prop3" , PropertyNumber = 103, Area = 80, PropertyID = 3, LandlordId = 3, Address = "Address3", PropertyStatus = Domain.Enums.Status.South},
                new Domain.Entities.Property { PropertyName = "prop4" , PropertyNumber = 104, Area = 90, PropertyID = 4, LandlordId = 1, Address = "Address4", PropertyStatus = Domain.Enums.Status.North}
            };
        }
    }
}
