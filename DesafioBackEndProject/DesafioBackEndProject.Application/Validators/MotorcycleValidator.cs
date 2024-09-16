using DesafioBackEndProject.Application.DTOs;
using FluentValidation;
namespace DesafioBackEndProject.Application.Validators
{
    public class MotorcycleValidator : AbstractValidator<MotorcycleCreateDto>
    {
        public MotorcycleValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("O modelo é obrigatório.")
                .MaximumLength(100).WithMessage("Model must not exceed 100 characters.");

            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("A placa é obrigatória.");
                //.Matches(@"^[A-Z]{3}-\d[A-Z]\d{2}$").WithMessage("A placa deve ser no formato AAA-AAAA.");


            RuleFor(x => x.ManufacturingYear)
                    .NotEmpty().WithMessage("O ano de fabricação é obrigatório.")
                   .GreaterThan(1900).WithMessage("O ano de fabricação deve ser maior que 1900.");

        }
    }
}
