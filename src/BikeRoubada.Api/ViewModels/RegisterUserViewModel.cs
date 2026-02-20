using System.ComponentModel.DataAnnotations;
namespace BikeRoubada.Api.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "o campo  {0} é obrigatóro")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo para o campo {0} é {1} de caracteres")]
        public string Nome {  get; set; }

        [Required(ErrorMessage = "o campo  {0} é obrigatóro")]
        [MaxLength(15, ErrorMessage = "O tamanho máximo para o campo {0} é {1} de caracteres")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "o campo  {0} é obrigatóro")]
        [EmailAddress(ErrorMessage ="O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "o campo  {0} é obrigatóro")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }



    }
}
