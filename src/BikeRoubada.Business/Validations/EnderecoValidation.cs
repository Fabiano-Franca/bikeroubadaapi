

using BikeRoubada.Business.Models;
using DocumentValidator;
using FluentValidation;
using Flunt.Br;
using Flunt.Br.Extensions;

namespace BikeRoubada.Business.Validations
{
     class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            
            
            var contract = new Contract();
            RuleFor(c => c.Cep.Length)
                .Equal(8).WithMessage("O Cep precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.Rua)
                .NotEmpty().WithMessage("O campo {PropertyName} é requerido");

            RuleFor(c => c.Rua)
                .MaximumLength(100)
                .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength}");

            RuleFor(c => c.Estado)
                .MaximumLength(50)
                .WithMessage("O campo {PropertyName} pode conter no máximo {MaxLength}");

        }
    }
}
