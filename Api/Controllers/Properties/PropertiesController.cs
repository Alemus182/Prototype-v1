using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Properties.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Web.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class PropertiesController : ControllerBase
    {
        #region Configurations

        private readonly IMediator _mediatR;
        public PropertiesController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        #endregion

        [HttpPost(ApiRoutes.Properties.CreatePropertyBuilding)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePropertyBuilding([FromBody] List<CreatePropertyWithOwnerRequest> requestData)
        {
            CreatePropertyBuildingRequest request = new CreatePropertyBuildingRequest();
            request.items = requestData;
            var result = await _mediatR.Send(request);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Properties.CreateProperty)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyRequest request)
        {
            var result = await _mediatR.Send(request);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Properties.CreatePropertyWithOwner)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePropertyWithOwner([FromBody] CreatePropertyWithOwnerRequest request)
        {
            var result = await _mediatR.Send(request);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Properties.AddImageProperty)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddImageProperty([FromBody] AddImagePropertyRequest request)
        {
            var result = await _mediatR.Send(request);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.Properties.ChangePriceProperty)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePriceProperty([FromBody] ChangePricePropertyRequest request)
        {
            var result = await _mediatR.Send(request);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Properties.FindPropertyByFilters)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FindPropertyByFilters([FromBody] FindPropertyByFiltersRequest request)
        {
            var result = await _mediatR.Send(request);
            return Ok(result);
        }
    }
}