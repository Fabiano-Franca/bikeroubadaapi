using FluentValidation;
using BikeRoubada.Business.Models;

namespace BikeRoubada.Business.Validations
{
    public class BicicletaValidation : AbstractValidator<Bicicleta>
    {
        public BicicletaValidation() 
        {
            RuleFor(c => c.Serial)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");
            RuleFor(c => c.LocalizacaoCadastro)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");
            RuleFor(c => c.IdEndereco)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");
            RuleFor(c => c.Descricao)
                .MaximumLength(300)
                .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength}");
        }
    }
}
