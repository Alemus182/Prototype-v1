using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Auth;
using Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Auth.Queries
{
    public class IniciarSesionRequest : IRequest<IniciarSesionResponse>
    {
        public string usuario { get; set; }
        public string contrasena { get; set; }

        public class SignInHandler : IRequestHandler<IniciarSesionRequest, IniciarSesionResponse>
        {
            private readonly ITokenService _tokenService;

            private readonly IConfiguration _configuration;

            public SignInHandler(ITokenService tokenService, IConfiguration configuration)
            {
                _tokenService = tokenService;
                _configuration = configuration;
            }

            public async Task<IniciarSesionResponse> Handle(IniciarSesionRequest request, CancellationToken cancellationToken)
            {
                IniciarSesionResponse response = new IniciarSesionResponse();
                response.idUsuario = request.usuario;
                response = GenerateAuthenticationResultForUserAsync(response);                
                return response;
            }

            private IniciarSesionResponse GenerateAuthenticationResultForUserAsync(IniciarSesionResponse response)
            {
                var usersClaims = new [] 
                {
                        new Claim(JwtRegisteredClaimNames.Sub, response.idUsuario),
                        new Claim(JwtRegisteredClaimNames.NameId,response.idUsuario),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, response.idUsuario),
                        //new Claim(ClaimTypes.Name, user),
                 };

                var token = _tokenService.GenerateAccessToken(usersClaims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                response.valido = true;
                response.token = token;
                response.refreshToken = refreshToken;
                return response;
            }
        }
    }
}