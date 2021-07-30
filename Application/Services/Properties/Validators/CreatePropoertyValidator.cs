using Application.Dtos.Properties;
using Application.Services.Properties.Commands;
using FluentValidation;
using System;

namespace Application.Services.Properties.Validators
{
    public class CreatePropoertyValidator : AbstractValidator<CreatePropertyRequest>
    {
        public CreatePropoertyValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(128);
            RuleFor(e => e.Address).NotEmpty().MaximumLength(128);
            RuleFor(e => e.Price).NotEmpty();
            RuleFor(e => e.Year).LessThanOrEqualTo(DateTime.Now.Year).GreaterThanOrEqualTo(1900).NotEmpty();
            RuleFor(e => e.IdOwner).NotEmpty();
        }
    }

    public class CreatePropoertyWithOwnerValidator : AbstractValidator<CreatePropertyWithOwnerRequest>
    {
        public CreatePropoertyWithOwnerValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(128);
            RuleFor(e => e.Address).NotEmpty().MaximumLength(128);
            RuleFor(e => e.Price).NotEmpty();
            RuleFor(e => e.Year).LessThanOrEqualTo(DateTime.Now.Year).GreaterThanOrEqualTo(1900).NotEmpty();
            RuleFor(e => e.NameOwner).NotEmpty().MaximumLength(128);
            RuleFor(e => e.AddressOwner).NotEmpty().MaximumLength(128);
            RuleFor(e => e.BirthdayOwner).NotEmpty().LessThanOrEqualTo(DateTime.Now.Date);
            RuleFor(e => new { e.PhotoOwner, e.PhotoOwnerType }).
                Must( x => ValidatePhotoOwner(x.PhotoOwner, x.PhotoOwnerType)).WithMessage("Photo owner invalid");
        }

        private bool ValidatePhotoOwner(string PhotoOwner, PhotoType ? PhotoOwnerType)
        {
            if (!string.IsNullOrEmpty(PhotoOwner) && !PhotoOwnerType.HasValue)
                return false;

            return true;
        }
    }
}
