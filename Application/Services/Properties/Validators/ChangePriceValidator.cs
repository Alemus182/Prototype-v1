using Application.Dtos.Properties;
using Application.Services.Properties.Commands;
using FluentValidation;
using System;

namespace Application.Services.Properties.Validators
{
    public class ChangePriceValidator : AbstractValidator<ChangePricePropertyRequest>
    {
        public ChangePriceValidator()
        {
            RuleFor(e => e.IdProperty).NotEmpty();
            RuleFor(e => e.NewPrice).NotEmpty().GreaterThan(0).LessThan(decimal.MaxValue);
        }
    }
}
