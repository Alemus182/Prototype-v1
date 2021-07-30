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
    public class AddImagePropertyRequest : IRequest<object>
    {
        public int IdProperty { get; set; }
        public string File { get; set; }
        public PhotoType PhotoOwnerType { get; set; }

        public class AddImagePropertyRequestHandler : IRequestHandler<AddImagePropertyRequest, object>
        {
            private readonly IPropertiesRepository _PropertiesRepository;

            private readonly ILogger<AddImagePropertyRequestHandler> _logger;

            private readonly ICurrentUserService _currentUserService;

            private readonly IFilePropertiesService _FilePropertiesService;

            public AddImagePropertyRequestHandler
                (
                IPropertiesRepository PropertiesRepository,
                ILogger<AddImagePropertyRequestHandler> logger,
                ICurrentUserService currentUserService,
                IFilePropertiesService FilePropertiesService
                )
            {
                _PropertiesRepository = PropertiesRepository;
                _logger = logger;
                _currentUserService = currentUserService;
                _FilePropertiesService = FilePropertiesService;
            }

            public async Task<object> Handle(AddImagePropertyRequest request, CancellationToken cancellationToken)
            {
                bool isvalid = true;
                string message = string.Empty;
                int idPropertyImage = 0;

                try
                {
                    string data = request.File;
                    
                    request.File = $"{Guid.NewGuid().ToString()}.{request.PhotoOwnerType.ToString()}";

                    idPropertyImage = await _PropertiesRepository.AddImageProperty(request);

                    if (idPropertyImage > 0)
                    {
                        var resultImage =  
                            await  _FilePropertiesService.
                            LoadPhotoProperty(request.IdProperty, data, request.File);

                        if (!resultImage.Item1)
                           await _PropertiesRepository.DeleteImageProperty(idPropertyImage);                        
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"userId {_currentUserService.UserId} - request CreatePropertyRequest - with error {ex.Message}");
                    isvalid = false;
                    message = $"Error save the record - {ex.Message}";
                }

                return new { isValid = isvalid, message = message, IdPropertyImage = idPropertyImage };
            }
        }
    }


}
