using BikeRoubada.Business.Models;
using FluentValidation;

namespace BikeRoubada.Business.Validations
{
    public class RouboValidation : AbstractValidator<Roubo>
    {
        public RouboValidation() 
        {
            RuleFor(c => c.DataRoubo)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.IdBicicleta)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.Localizacao)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.Relato)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");
        }
    }
}
