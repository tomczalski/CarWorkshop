using CarWorkshop.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkshopCommandValidator(ICarWorkshopRepository repository)
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(20).Custom((value, context) =>
            {
                var existingCarWorkshop = repository.GetByName(value).Result;
                if (existingCarWorkshop != null)
                {
                    context.AddFailure($"Warsztat {value} znajduje się już w bazie");
                }
            });

            RuleFor(c => c.Description).NotEmpty().WithMessage("Pole nie może byc puste!");

            RuleFor(c => c.PhoneNumber).MinimumLength(8).MaximumLength(12);
        }
    }
}
