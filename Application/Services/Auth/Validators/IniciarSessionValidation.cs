using System;
using FluentValidation;
using Application.Services.Auth.Queries;

namespace Application.Services.Auth.Validators
{
    public class IniciarSessionValidation : AbstractValidator<IniciarSesionRequest>
    {
        public IniciarSessionValidation()
        {
            RuleFor(e => e.usuario).NotEmpty().EmailAddress();
            RuleFor(e => e.contrasena).NotEmpty().MinimumLength(8);
        }
    }
}
