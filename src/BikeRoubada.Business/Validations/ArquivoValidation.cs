using BikeRoubada.Business.Models;
using FluentValidation;
using FluentValidation.Validators;


namespace BikeRoubada.Business.Validations
{
    public class ArquivoValidation : AbstractValidator<Arquivo>
    {
        public ArquivoValidation() 
        {
            RuleFor(c => c.NomeArquivo)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.NomeArquivo)
                .MaximumLength(50)
                .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength}");
        }
    }
}
