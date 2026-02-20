using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BikeRoubada.Api.ViewModels
{
    public class BicicletaViewModel 
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Serial { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Descricao { get; set; }
        public string? Detalhes { get; set; }
        public Point? LocalizacaoCadastro { get; set;}
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid IdEndereco { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid IdUsuario { get; set; }
        public EnderecoViewModel? Endereco { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public List<RouboViewModel>? Roubos { get; set; }
        public List<ArquivoViewModel>? Arquivos { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime DataCadastro { get; set; }
    }
}
