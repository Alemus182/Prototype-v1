using Application.Common.Interfaces;
using Application.Dtos.Properties;
using Application.Interfaces.Infraestructure.Data;
using Application.Interfaces.Infraestructure.Services.Properties;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Properties.Commands
{
    public class CreatePropertyWithOwnerRequest : IRequest<object>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string NameOwner { get; set; }
        public string AddressOwner { get; set; }
        public string PhotoOwner { get; set; }
        public PhotoType ? PhotoOwnerType { get; set; }
        public DateTime BirthdayOwner { get; set; }

        public class CreatePropertyWithOwnerRequestHandler : IRequestHandler<CreatePropertyWithOwnerRequest, object>
        {
            private readonly IPropertiesRepository _PropertiesRepository;

            private readonly ILogger<CreatePropertyWithOwnerRequestHandler> _logger;

            private readonly ICurrentUserService _currentUserService;

            private readonly IFilePropertiesService _FilePropertiesService;

            public CreatePropertyWithOwnerRequestHandler
                (
                IPropertiesRepository PropertiesRepository, 
                ILogger<CreatePropertyWithOwnerRequestHandler> logger,
                ICurrentUserService currentUserService,
                IFilePropertiesService FilePropertiesService
                )
            {
                _PropertiesRepository = PropertiesRepository;
                _logger = logger;
                _currentUserService = currentUserService;
                _FilePropertiesService = FilePropertiesService;
            }

            public async Task<object> Handle(CreatePropertyWithOwnerRequest request, CancellationToken cancellationToken)
            {
                bool isvalid = true;
                string message = string.Empty;
                int idProperty = 0;

                try
                {
                    string data = request.PhotoOwner;

                    if (! string.IsNullOrEmpty(request.PhotoOwner))
                        request.PhotoOwner = $"{Guid.NewGuid().ToString()}.{request.PhotoOwnerType}";
                    
                    var resultSave =  await _PropertiesRepository.CreatePropertyWithOwner(request);

                    if (string.IsNullOrEmpty(resultSave.Item1))
                    {
                        isvalid = false;
                        message = "Record no save";
                    }
                    else
                    {
                        idProperty = int.Parse(resultSave.Item1);

                        if (! string.IsNullOrEmpty(data))
                        {
                            int idOwner = int.Parse(resultSave.Item2);
                            
                            var resultsavePhoto = 
                                await _FilePropertiesService.
                                LoadPhotoOwner(idOwner, data, request.PhotoOwner);

                            isvalid = resultsavePhoto.Item1;
                            message = resultsavePhoto.Item2;

                            if (!isvalid)
                              await  _PropertiesRepository.DeleteProperty(idProperty,true); 
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"userId {_currentUserService.UserId} - request CreatePropertyRequest - with error {ex.Message}");
                    isvalid = false;
                    message = $"Error save the record - {ex.Message}";
                }

                return new { isValid = isvalid, message = message, idProperty = idProperty};
            }
        }
    }
}
