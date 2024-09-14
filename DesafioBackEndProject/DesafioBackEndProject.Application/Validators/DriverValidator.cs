using DesafioBackEndProject.Application.DTOs;
using FluentValidation;
namespace DesafioBackEndProject.Application.Validators
{
    public class DriverValidator : AbstractValidator<DriverCreateDto>
    {
        public DriverValidator()
        {
            RuleFor(x => x.Identificador)
                .NotEmpty().WithMessage("O modelo é obrigatório.")
                .MaximumLength(100).WithMessage("Model must not exceed 100 characters.");

            RuleFor(x => x.NumeroCnh)
                .NotEmpty().WithMessage("A placa é obrigatória.");

            RuleFor(x => x.TipoCnh)
                .NotEmpty().WithMessage("O Tipo de CNH é obrigatório.")
                .Must(cnh => cnh == "A" || cnh == "B" || cnh == "A+B")
                .WithMessage("O tipo de CNH deve ser A, B ou A+B.");

            RuleFor(x => x.Cnpj)
                    .NotEmpty().WithMessage("O ano de fabricação é obrigatório.")
                   ;

        }
    }
}
