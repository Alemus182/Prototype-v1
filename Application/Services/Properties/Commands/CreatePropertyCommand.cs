using Application.Common.Interfaces;
using Application.Interfaces.Infraestructure.Data;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Properties.Commands
{
    public class CreatePropertyRequest : IRequest<object>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public class CreatePropertyRequestHandler : IRequestHandler<CreatePropertyRequest, object>
        {
            private readonly IPropertiesRepository _PropertiesRepository;

            private readonly ILogger<CreatePropertyRequestHandler> _logger;

            private readonly ICurrentUserService _currentUserService;

            public CreatePropertyRequestHandler
                (
                IPropertiesRepository PropertiesRepository, 
                ILogger<CreatePropertyRequestHandler> logger,
                ICurrentUserService currentUserService
                )
            {
                _PropertiesRepository = PropertiesRepository;
                _logger = logger;
                _currentUserService = currentUserService;
            }

            public async Task<object> Handle(CreatePropertyRequest request, CancellationToken cancellationToken)
            {
                bool isvalid = true;
                string message = string.Empty;

                try
                {
                    var resultSave =  await _PropertiesRepository.CreateProperty(request);

                    if (resultSave < 0)
                    {
                        isvalid = false;
                        message = "Record no save";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"userId {_currentUserService.UserId} - request CreatePropertyRequest - with error {ex.Message}");
                    isvalid = false;
                    message = $"Error save the record - {ex.Message}";
                }

                return new { isValid = isvalid, message = message};
            }
        }
    }
}
