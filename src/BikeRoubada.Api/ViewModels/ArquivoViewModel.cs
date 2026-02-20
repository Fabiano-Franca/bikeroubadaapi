using BikeRoubada.Business.Enums;
using System.ComponentModel.DataAnnotations;

namespace BikeRoubada.Api.ViewModels
{
    public class ArquivoViewModel 
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NomeArquivo { get; set; }
        public Guid? IdRoubo { get; set; }
        public RouboViewModel? Roubo { get; set; }
        public Guid? IdBicicleta { get; set; }
        public BicicletaViewModel? Bicicleta { get; set; }
        public TipoArquivo Tipo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
