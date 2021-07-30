using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IConfiguration _configuration;


        public CurrentUserService
            (
            IHttpContextAccessor httpContextAccessor, 
             IConfiguration configuration
            )
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Username = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            IsAuthenticated = UserId != null;
            _configuration = configuration;
        }

        public string UserId { get; }
        public string Username { get; }
        public bool IsAuthenticated { get; }
    }
}
