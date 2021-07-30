using Application.Common.Interfaces;
using Application.Interfaces.Infraestructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Properties.Commands
{
    public class ChangePricePropertyRequest : IRequest<object>
    {
        public int IdProperty { get; set; }
        public decimal NewPrice { get; set; }

        public class ChangePricePropertyRequestHandler : IRequestHandler<ChangePricePropertyRequest, object>
        {
            private readonly IPropertiesRepository _PropertiesRepository;

            private readonly ILogger<ChangePricePropertyRequest> _logger;

            private readonly ICurrentUserService _currentUserService;

            public ChangePricePropertyRequestHandler
                (
                IPropertiesRepository PropertiesRepository,
                ILogger<ChangePricePropertyRequest> logger,
                ICurrentUserService currentUserService
                )
            {
                _PropertiesRepository = PropertiesRepository;
                _logger = logger;
                _currentUserService = currentUserService;
            }

            public async Task<object> Handle(ChangePricePropertyRequest request, CancellationToken cancellationToken)
            {
                bool isvalid = true;
                string message = string.Empty;

                try
                { 
                    var result = await _PropertiesRepository.ChangePriceProperty(request);
                    if (result == 0)
                    {
                        isvalid = false;
                        message = $"Error save the record";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"userId {_currentUserService.UserId} - request ChangePricePropertyRequest - with error {ex.Message}");
                    isvalid = false;
                    message = $"Error save the record - {ex.Message}";
                }

                return new { isValid = isvalid, message = message };
            }
        }
    }
}
