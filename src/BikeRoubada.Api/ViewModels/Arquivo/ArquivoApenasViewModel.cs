using BikeRoubada.Business.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BikeRoubada.Api.ViewModels.Arquivo
{
    public class ArquivoApenasViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid Id { get; set; }
        public string NomeArquivo { get; set; }
        public TipoArquivo Tipo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? IdRoubo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? IdBicicleta { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime DataCadastro { get; set; }
    }
}
