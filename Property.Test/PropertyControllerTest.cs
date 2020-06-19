using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Property.Controllers;
using Property.DataAccess;
using Property.Domain.DTOs;
using Property.Provider;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Property.Test
{
    public class PropertyControllerTest
    {
        readonly PropertyController _controller;
        private readonly Mock<IGenericEfRepository<Domain.Entities.Property>> _mockRepo;
        readonly IMapper mapper;
        public PropertyControllerTest()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            mapper = mappingConfig.CreateMapper();

            _mockRepo = new Mock<IGenericEfRepository<Domain.Entities.Property>>();

            IPropertyProvider provider = new PropertyProvider(_mockRepo.Object, mapper);
            
            _controller = new PropertyController(provider);

            _mockRepo.Setup(m => m.Get())
                .Returns(Task.FromResult(MockData.Current.Properties.AsEnumerable()));
        }

        private void MoqSetup(int propertyId)
        {
            _mockRepo.Setup(x => x.Get(It.Is<int>(y => y == propertyId), false))
                .Returns(Task.FromResult(MockData.Current.Properties
                    .FirstOrDefault(p => p.PropertyID.Equals(propertyId))));
        }

        private void MoqSetupAdd(Domain.Entities.Property testItem)
        {
            _mockRepo.Setup(x => x.Add(It.Is<Domain.Entities.Property>(y => y == testItem)))
                .Callback<Domain.Entities.Property>(s => MockData.Current.Properties.Add(s));
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            const int testLandlordId = 2;

            // Act
            var okResult = await _controller.Get(testLandlordId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            const int testLandlordId = 1;

            // Act
            var okResult = await _controller.Get(testLandlordId) as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<PropertyDto>>(okResult?.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetById_UnknownPropertyIdPassed_ReturnsNotFoundResult()
        {
            //Arrange
            MoqSetup(50);

            // Act
            var notFoundResult = await _controller.Get(1, 50);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetById_UnknownLandlordIdAndPropertyIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = await _controller.Get(4, 8);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetById_ExistingLandlordIdAndPropertyIdPassed_ReturnsOkResult()
        {
            // Arrange
            const int testLandlordId = 2;
            const int testPropertyId = 2;
            MoqSetup(2);

            // Act
            var okResult = await _controller.Get(testLandlordId, testPropertyId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetById_ExistingLandlordIdAndPropertyIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testLandlordId = 2;
            var testPropertyId = 2;
            MoqSetup(2);

            // Act
            var okResult = await _controller.Get(testLandlordId, testPropertyId) as OkObjectResult;

            // Assert
            Assert.IsType<PropertyDto>(okResult?.Value);
            Assert.Equal(testLandlordId, ((PropertyDto)okResult.Value).LandlordId);
            Assert.Equal(testPropertyId, ((PropertyDto)okResult.Value).PropertyId);

        }

        [Fact]
        public void Add_NullObjectPassed_ReturnsBadRequest()
        {
            // Act
            var badResponse = _controller.Post(0, null);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Add_Property_ReturnsCreatedResponse()
        {
            // Arrange
            var theItem = new Domain.Entities.Property
            {
                PropertyName = "prop5",
                PropertyNumber = 105,
                Area = 100,
                LandlordId = 1,
                PropertyID = 5,
                Address = "Address5",
                PropertyStatus = Domain.Enums.Status.North
            };

            MoqSetupAdd(theItem);

            var createdDto = mapper.Map<PropertyDto>(theItem);

            // Act

            //See how the ValidateViewModel extension method in the Helper class is useful here
            _controller.ValidateViewModel(theItem);
            //I have used the above useful extension method to simulate validation instead of adding customly like below
            //_controller.ModelState.AddModelError("CustomerName", "Required");

            var theResponse = _controller.Post(1, createdDto);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(theResponse);
        }

        [Fact]
        public void Add_MissingOPropertyNamePassed_ReturnsBadRequest()
        {
            // Arrange
            var theItem = new Domain.Entities.Property
            {
                PropertyNumber = 105,
                Area = 100,
                LandlordId = 1,
                PropertyID = 5,
                Address = "Address5",
                PropertyStatus = Domain.Enums.Status.North
            };

            MoqSetupAdd(theItem);
            var createdDto = mapper.Map<PropertyDto>(theItem);

            // Act

            //See how the ValidateViewModel extension method in the Helper class is useful here
            _controller.ValidateViewModel(theItem);
            //I have used the above useful extension method to simulate validation instead of adding customly like below
            //_controller.ModelState.AddModelError("PropertyName", "Required");

            var badResponse = _controller.Post(1, createdDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
