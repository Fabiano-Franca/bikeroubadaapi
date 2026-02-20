using BikeRoubada.Api.AuxiliaryModels;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BikeRoubada.Api.ViewModels.Bicicleta
{
    public class BicicletaApenasViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Serial { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Descricao { get; set; }
        public string? Detalhes { get; set; }
        public SimplePoint? LocalizacaoCadastro { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid IdEndereco { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid IdUsuario { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime DataCadastro { get; set; }
    }
}
