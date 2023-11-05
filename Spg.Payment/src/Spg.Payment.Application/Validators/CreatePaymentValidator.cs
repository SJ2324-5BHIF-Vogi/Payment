using FluentValidation;
using Spg.Payment.DomainModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.Application.Validators
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.PaidForWhat)
                .NotEmpty().WithMessage("Darf nicht leer sein.");

            RuleFor(x => x.ValidFrom)
                .NotEmpty().WithMessage("Darf nicht leer sein.");

            RuleFor(x => x.ValidTill)
                .NotEmpty().WithMessage("Darf nicht leer sein.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Darf nicht leer sein.");

            RuleFor(x => x.Price)
                .LessThanOrEqualTo(0).WithMessage("Preis darf nicht kleiner gleich 0 sein.");

            RuleFor(x => x.Discount)
                .GreaterThan(x => x.Price).WithMessage("Discount darf nicht größer als der Preis sein");
        }
    }
}
