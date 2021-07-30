using Application.Dtos.Properties;
using Application.Services.Properties.Commands;
using FluentValidation;
using System;

namespace Application.Services.Properties.Validators
{
    public class FindPropertiesValidator : AbstractValidator<FindPropertyByFiltersRequest>
    {
        public FindPropertiesValidator()
        {
            RuleFor(e => e.Address).MaximumLength(128).MinimumLength(3);
            RuleFor(e => e.Name).MaximumLength(128).MinimumLength(3);
            RuleFor(e => e.OwnerName).MaximumLength(128).MinimumLength(3);
            RuleFor(e => e.YearLessOrEqual).LessThan(DateTime.Now.Year);
            RuleFor(e => e.YearHigherOrEqual).LessThan(DateTime.Now.Year);
            RuleFor(e => new { e.YearLessOrEqual, e.YearHigherOrEqual }).
                   Must(x => ValidateYearRange(x.YearLessOrEqual, x.YearHigherOrEqual)).WithMessage("yearless must be greater than or equal yeargreater ");
            RuleFor(e => new { e.PriceLessOrEqual, e.PriceHigherOrEqual }).
                   Must(x => ValidatePriceRange(x.PriceLessOrEqual, x.PriceHigherOrEqual)).WithMessage("priceless must be greater than or equal pricegreater ");
        }

        private bool ValidateYearRange(int ? YearLess, int? Yeargreater)
        {
            if (YearLess.HasValue && Yeargreater.HasValue)
            {
                if (YearLess < Yeargreater)
                    return false;
            }

            return true;
        }

        private bool ValidatePriceRange(decimal? PriceLess, decimal? PriceGreater)
        {
            if (PriceLess.HasValue && PriceGreater.HasValue)
            {
                if (PriceLess < PriceGreater)
                    return false;
            }

            return true;
        }
    }
}
