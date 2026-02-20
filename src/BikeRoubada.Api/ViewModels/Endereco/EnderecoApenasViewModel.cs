using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BikeRoubada.Api.ViewModels.Endereco
{
    public class EnderecoApenasViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Rua { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Bairro { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Complemento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Cep { get; set; }
        public bool Principal { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid? IdUsuario { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime DataCadastro { get; set; }
    }
}
