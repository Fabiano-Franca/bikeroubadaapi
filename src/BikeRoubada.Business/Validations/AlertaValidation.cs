using BikeRoubada.Business.Models;
using FluentValidation;

namespace BikeRoubada.Business.Validations
{
    public class AlertaValidation : AbstractValidator<Alerta>
    {

        public AlertaValidation() 
        {

            RuleFor(c => c.IdBicicleta)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.Localizacao)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.IdUsuarioGerador)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

        }
    }
}
