using BikeRoubada.Business.Models;
using FluentValidation;

namespace BikeRoubada.Business.Validations
{
    public class TipoAlertaValidation : AbstractValidator<TipoAlerta>
    {
        public TipoAlertaValidation() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} é requerido")
                .MaximumLength(30)
                    .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength} caracteres");

        }
    }
}
