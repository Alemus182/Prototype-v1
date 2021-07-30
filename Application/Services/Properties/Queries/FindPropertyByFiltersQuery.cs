using Application.Common.Interfaces;
using Application.Dtos.Properties;
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
    public class FindPropertyByFiltersRequest : IRequest<object>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal ? PriceLessOrEqual { get; set; }
        public decimal ? PriceHigherOrEqual { get; set; }
        public int ? YearLessOrEqual { get; set; }
        public int ? YearHigherOrEqual { get; set; }
        public string  OwnerName { get; set; }

        public class FindPropertiesByFiltersRequestHandler : IRequestHandler<FindPropertyByFiltersRequest, object>
        {
            private readonly IPropertiesRepository _PropertiesRepository;

            private readonly ILogger<FindPropertiesByFiltersRequestHandler> _logger;

            private readonly ICurrentUserService _currentUserService;

            public FindPropertiesByFiltersRequestHandler
                (
                IPropertiesRepository PropertiesRepository,
                ILogger<FindPropertiesByFiltersRequestHandler> logger,
                ICurrentUserService currentUserService
                )
            {
                _PropertiesRepository = PropertiesRepository;
                _logger = logger;
                _currentUserService = currentUserService;
            }

            public async Task<object> Handle(FindPropertyByFiltersRequest request, CancellationToken cancellationToken)
            {
                IEnumerable<FindPropertiesResponse> resultList = null;
                bool isValid = true;
                string message = string.Empty;
                try
                {
                    resultList = await _PropertiesRepository.FindPropertiesByFilters(request);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"userId {_currentUserService.UserId} - request FindPropertyByFiltersRequest - with error {ex.Message}");
                    isValid = false;
                    message = $"Error save the record - {ex.Message}";
                }

                return new { isValid = isValid, message = message, data = resultList };
            }
        }
    }


}
