using Application.Common.Interfaces;
using Application.Interfaces.Infraestructure.Data;
using Application.Interfaces.Infraestructure.Services.Properties;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Properties.Commands
{
    public class CreatePropertyBuildingRequest : IRequest<List<object>>
    {
        public List<CreatePropertyWithOwnerRequest> items { get; set; }

        public class CreatePropertyBuildingRequestHandler : IRequestHandler<CreatePropertyBuildingRequest,List<object>>
        {
            private readonly IPropertiesRepository _PropertiesRepository;

            private readonly ILogger<CreatePropertyBuildingRequestHandler> _logger;

            private readonly ICurrentUserService _currentUserService;

            private readonly IFilePropertiesService _FilePropertiesService;

            public CreatePropertyBuildingRequestHandler
                (
                IPropertiesRepository PropertiesRepository,
                ILogger<CreatePropertyBuildingRequestHandler> logger,
                ICurrentUserService currentUserService,
                IFilePropertiesService FilePropertiesService
                )
            {
                _PropertiesRepository = PropertiesRepository;
                _logger = logger;
                _currentUserService = currentUserService;
                _FilePropertiesService = FilePropertiesService;
            }

            public async Task<List<object>> Handle(CreatePropertyBuildingRequest request, CancellationToken cancellationToken)
            {
                List<object> result = new List<object>();

                bool isvalid;
                string message;
                int idProperty;
                int index = 0;


                foreach (var item in request.items)
                {
                    try
                    {
                        isvalid = true;
                        message = string.Empty;
                        idProperty = 0;

                        string data = item.PhotoOwner;

                        if (!string.IsNullOrEmpty(item.PhotoOwner))
                            item.PhotoOwner = $"{Guid.NewGuid().ToString()}.{item.PhotoOwnerType}";

                        var resultSave = await _PropertiesRepository.CreatePropertyWithOwner(item);

                        if (string.IsNullOrEmpty(resultSave.Item1))
                        {
                            isvalid = false;
                            message = "Record no save";
                        }
                        else
                        {
                            idProperty = int.Parse(resultSave.Item1);

                            if (!string.IsNullOrEmpty(data))
                            {
                                int idOwner = int.Parse(resultSave.Item2);

                                var resultsavePhoto =
                                    await _FilePropertiesService.
                                    LoadPhotoOwner(idOwner, data, item.PhotoOwner);

                                isvalid = resultsavePhoto.Item1;
                                message = resultsavePhoto.Item2;

                                if (!isvalid)
                                    await _PropertiesRepository.DeleteProperty(idProperty, true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"userId {_currentUserService.UserId} - request CreatePropertyRequest - with error {ex.Message}");
                        isvalid = false;
                        message = $"Error save the record - {ex.Message}";
                    }

                    result.Add(new { index = index, isvalid = isvalid, message = message, });

                    index++;
                }


                return result;
            }
        }
    }
}
