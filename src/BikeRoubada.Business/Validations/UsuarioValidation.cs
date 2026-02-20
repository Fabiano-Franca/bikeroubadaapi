using BikeRoubada.Business.Models;
using DocumentValidator;
using FluentValidation;

namespace BikeRoubada.Business.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.Nome)
                .MaximumLength(150)
                .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength}");

            RuleFor(c => c.Genero)
                .MaximumLength(20)
                .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength}");
            RuleFor(c => c.Email)
                .MaximumLength(256)
                .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength}");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("O campo {PropertyName} não está no formato adequado");

            RuleFor(c => c.Telefone)
                .MaximumLength(15)
                .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength}");
        }
    }
}
