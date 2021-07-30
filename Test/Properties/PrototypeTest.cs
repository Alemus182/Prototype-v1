using Application.Common.Interfaces;
using Application.Dtos.Properties;
using Application.Interfaces.Infraestructure.Data;
using Application.Interfaces.Infraestructure.Services.Properties;
using Application.Services.Properties.Commands;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Application.Services.Properties.Commands.AddImagePropertyRequest;
using static Application.Services.Properties.Commands.ChangePricePropertyRequest;
using static Application.Services.Properties.Commands.CreatePropertyBuildingRequest;
using static Application.Services.Properties.Commands.CreatePropertyRequest;
using static Application.Services.Properties.Commands.CreatePropertyWithOwnerRequest;
using static Application.Services.Properties.Commands.FindPropertyByFiltersRequest;

namespace Test
{
    public class PrototypeTest
    {
        private  Mock<IPropertiesRepository> _PropertiesRepository;
        private  Mock<ICurrentUserService> _currentUserService;
        private  Mock<IFilePropertiesService> _FilePropertiesService;

        [SetUp]
        public void Setup()
        {
            _PropertiesRepository = new Mock<IPropertiesRepository>();
            _currentUserService = new Mock<ICurrentUserService>();
            _FilePropertiesService = new Mock<IFilePropertiesService>();
        }

        [Test]
        public async Task ChangePricePropertyRequestHandlerTest()
        {
           Mock<ILogger<ChangePricePropertyRequest>> _logger = new Mock<ILogger<ChangePricePropertyRequest>>();

            var request = new ChangePricePropertyRequest() { IdProperty = 16, NewPrice = 10000000 };

            var fakeChangePriceProperty = FakeChangePriceProperty(request);

            _PropertiesRepository.Setup(x => x.ChangePriceProperty(request))
            .Returns(Task.FromResult(fakeChangePriceProperty));

            ChangePricePropertyRequestHandler handler = new ChangePricePropertyRequestHandler                
                (
                 _PropertiesRepository.Object,
                 _logger.Object,
                 _currentUserService.Object                
                );

           var result =  
                await handler.Handle
                (request, CancellationToken.None );

            var isValid = bool.Parse(result.GetType().GetProperty("isValid").GetValue(result,null).ToString());

            Assert.IsTrue(isValid,"Update Price Correct");
        }

        [Test]
        public async Task AddImagePropertyRequestHandlerTest()
        {
            Mock<ILogger<AddImagePropertyRequestHandler>> _logger = new Mock<ILogger<AddImagePropertyRequestHandler>>();

            var request = new AddImagePropertyRequest() 
            { IdProperty = 16, File = "base64stream", PhotoOwnerType = Application.Dtos.Properties.PhotoType.png };

            var fakeAddImage = FakeAddImageProperty(request);

            _PropertiesRepository.Setup(x => x.AddImageProperty(request))
            .Returns(Task.FromResult(fakeAddImage));

            var fakeloadImagestorage = FakeLoadImage(0, string.Empty, string.Empty);

            _FilePropertiesService.Setup
                (x => x.LoadPhotoProperty(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.FromResult(fakeloadImagestorage)); 
            
            AddImagePropertyRequestHandler handler = new AddImagePropertyRequestHandler
                (
                 _PropertiesRepository.Object,
                 _logger.Object,
                 _currentUserService.Object,
                 _FilePropertiesService.Object
                );

            var result =
                 await handler.Handle
                 (request, CancellationToken.None);

            var isValid = bool.Parse(result.GetType().GetProperty("isValid").GetValue(result, null).ToString());

            Assert.IsTrue(isValid, "Load Image Correct");
        }

        [Test]
        public async Task CreatePropertyBuildingRequestTest()
        {
            Mock<ILogger<CreatePropertyBuildingRequestHandler>> _logger = new Mock<ILogger<CreatePropertyBuildingRequestHandler>>();

            var request = GetCreatePropertyBuildingRequest();

            var fakeCreateProperty = CreatePropertyWithOwner(new CreatePropertyWithOwnerRequest());

            _PropertiesRepository.
                Setup(x => x.CreatePropertyWithOwner(It.IsAny<CreatePropertyWithOwnerRequest>()))
                 .Returns(Task.FromResult(fakeCreateProperty));

            _PropertiesRepository.
                Setup(x => x.DeleteProperty(It.IsAny<int>(),It.IsAny<bool>()))
                .Returns(Task.FromResult(1));

            var fakeloadImagestorage = FakeLoadImage(0, string.Empty, string.Empty);

            _FilePropertiesService.Setup
                (x => x.LoadPhotoOwner(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.FromResult(fakeloadImagestorage));

            CreatePropertyBuildingRequestHandler handler = new CreatePropertyBuildingRequestHandler
                (
                 _PropertiesRepository.Object,
                 _logger.Object,
                 _currentUserService.Object,
                 _FilePropertiesService.Object
                );

            List<object> result =
                 await handler.Handle
                 (request, CancellationToken.None);
           
            Assert.IsNotNull(result, "Create building correct not null");

            if (result != null)
            {
                var countResult = result.Count();
                var countSend = request.items.Count();

                Assert.AreEqual(countSend, countResult);
            }
        }

        [Test]
        public async Task CreatePropertyWithOwnerRequestHandlerTest()
        {
            Mock<ILogger<CreatePropertyWithOwnerRequestHandler>> _logger = new Mock<ILogger<CreatePropertyWithOwnerRequestHandler>>();

            var request = new CreatePropertyWithOwnerRequest() { Address = "", NameOwner = "", AddressOwner = " ", Name = "", Price = 100000, Year = 2022, PhotoOwner="", BirthdayOwner= DateTime.Now, PhotoOwnerType= Application.Dtos.Properties.PhotoType.bmp };

            var fakeCreateProperty = CreatePropertyWithOwner(new CreatePropertyWithOwnerRequest());

            _PropertiesRepository.
                Setup(x => x.CreatePropertyWithOwner(It.IsAny<CreatePropertyWithOwnerRequest>()))
                 .Returns(Task.FromResult(fakeCreateProperty));

            _PropertiesRepository.
                Setup(x => x.DeleteProperty(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(1));

            var fakeloadImagestorage = FakeLoadImage(0, string.Empty, string.Empty);

            _FilePropertiesService.Setup
                (x => x.LoadPhotoOwner(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.FromResult(fakeloadImagestorage));

            CreatePropertyWithOwnerRequestHandler handler = new CreatePropertyWithOwnerRequestHandler
                (
                 _PropertiesRepository.Object,
                 _logger.Object,
                 _currentUserService.Object, 
                 _FilePropertiesService.Object
                );

            var result =
                 await handler.Handle
                 (request, CancellationToken.None);

            var isValid = bool.Parse(result.GetType().GetProperty("isValid").GetValue(result, null).ToString());

            Assert.IsTrue(isValid, "Create property command");
        }

        [Test]
        public async Task FindPropertyByFiltersRequestHandlerTest()
        {
            Mock<ILogger<FindPropertiesByFiltersRequestHandler>> _logger = new Mock<ILogger<FindPropertiesByFiltersRequestHandler>>();

            var request = new FindPropertyByFiltersRequest() { Address = "", OwnerName = "",  Name = "" };

            IEnumerable<FindPropertiesResponse> fakeListProperty = ListPropertyWithOwner(request);

            _PropertiesRepository.
                Setup(x => x.FindPropertiesByFilters(It.IsAny<FindPropertyByFiltersRequest>()))
                 .Returns(Task.FromResult(fakeListProperty));

            FindPropertiesByFiltersRequestHandler handler = new FindPropertiesByFiltersRequestHandler
                (
                 _PropertiesRepository.Object,
                 _logger.Object,
                 _currentUserService.Object
                );

            var result =
                 await handler.Handle
                 (request, CancellationToken.None);

            var isValid = bool.Parse(result.GetType().GetProperty("isValid").GetValue(result, null).ToString());

            Assert.IsTrue(isValid, "List properties correct");
        }

        
        #region Fake Functions

        private int FakeChangePriceProperty(ChangePricePropertyRequest request)
        {
            return 1;
        }

        private int FakeAddImageProperty(AddImagePropertyRequest request)
        {
            return 1;
        }

        private Tuple<bool,string> FakeLoadImage(int IdProperty, string data, string filename)
        {
            return new Tuple<bool, string>(true, string.Empty);
        }

        private IEnumerable<FindPropertiesResponse> ListPropertyWithOwner(FindPropertyByFiltersRequest request)
        {
            List<FindPropertiesResponse> result = new List<FindPropertiesResponse>()
            {
                 new FindPropertiesResponse(),
                 new FindPropertiesResponse()
            };

            return result;
        }


        private Tuple<string, string> CreatePropertyWithOwner(CreatePropertyWithOwnerRequest createPropertyWithOwnerRequest)
        {
            return new Tuple<string, string>("1", "1");
        }

        private CreatePropertyBuildingRequest GetCreatePropertyBuildingRequest()
        {
            CreatePropertyBuildingRequest request = new CreatePropertyBuildingRequest();
            request.items = new System.Collections.Generic.List<CreatePropertyWithOwnerRequest>()
             {
                 new CreatePropertyWithOwnerRequest
                 {

                 },
                 new CreatePropertyWithOwnerRequest
                 {

                 }
             };

            return request;
        }


        #endregion
    }
}