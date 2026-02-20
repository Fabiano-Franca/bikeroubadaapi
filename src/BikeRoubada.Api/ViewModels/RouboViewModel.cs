using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace BikeRoubada.Api.ViewModels
{ 
    public class RouboViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Relato { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataRoubo {  get; set; }
        public DateTime? DataRecuperacao { get; set; }
        public  Point Localizacao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? NumeroBoletim {  get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid IdBicicleta { get; set; }
        public BicicletaViewModel Bicicleta { get; set; }
        public List<ArquivoViewModel> Arquivos { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
