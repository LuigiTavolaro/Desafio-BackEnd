using DesafioBackEndProject.Application.DTOs;
using FluentValidation;
namespace DesafioBackEndProject.Application.Validators
{
    public class RentalValidator : AbstractValidator<RentalCreateDto>
    {
        public RentalValidator()
        {


            RuleFor(x => x.DataInicio)
                .NotEmpty().WithMessage("A data de início é obrigatória.")
                .Must(SerUmDiaAposHoje).WithMessage("A data informada deve ser um dia após hoje.");

            // Regra para garantir que a DataTermino é obrigatória
            RuleFor(x => x.DataTermino)
                .NotEmpty().WithMessage("A data de término é obrigatória.");

            // Regra para garantir que a PrevisaoTermino é obrigatória
            RuleFor(x => x.DataPrevisaoTermino)
                .NotEmpty().WithMessage("A data de previsão de término é obrigatória.");

            // Regra adicional para garantir que DataTermino seja posterior ou igual à DataInicio
            RuleFor(x => x)
                .Must(x => x.DataInicio <= x.DataTermino)
                .WithMessage("A data de término deve ser posterior ou igual à data de início.");

            // Regra adicional para garantir que PrevisaoTermino seja anterior ou igual à DataTermino
            RuleFor(x => x)
                .Must(x => x.DataPrevisaoTermino <= x.DataTermino)
                .WithMessage("A data de previsão de término deve ser anterior ou igual à data de término.");


        }
        private bool SerUmDiaAposHoje(DateTime data)
        {
            return data.Date == DateTime.Today.AddDays(1);
        }
    }
}
