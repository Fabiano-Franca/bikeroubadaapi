using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BikeRoubada.Api.ViewModels.Usuario
{
    public class UsuarioApenasViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid Id { get; set; }
        public string FotoPerfil { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public string IdentificadorPessoal { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [EmailAddress(ErrorMessage = "O campo {0} não está no formato adequado")]
        public string Email { get; set; }
        public string? Genero { get; set; }
        [Required(ErrorMessage = "O campo {0} é requerido")]
        public int TipoPessoa { get; set; }
        public string Telefone { get; set; }

        public IFormFile? FotoPerfilFormContent { get; set; }
    }
}
