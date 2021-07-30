using System.Threading.Tasks;
using Application.Services.Auth.Queries;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers.v1
{
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        #region Configurations

        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediatR;
        private readonly ICurrentUserService _currentUserService;
        public AuthController(ILogger<AuthController> logger, IMediator mediatR, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _mediatR = mediatR;
            _currentUserService = currentUserService;
        }

        #endregion

        [HttpPost(ApiRoutes.Auth.Init)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] IniciarSesionRequest iniciarSesion)
        {
            var result = await _mediatR.Send(iniciarSesion);
            return Ok(result);
        }
    }
}