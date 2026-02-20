using System.ComponentModel.DataAnnotations;

namespace BikeRoubada.Api.ViewModels
{
    public class TipoAlertaViewModel 
    {
        Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Nome { get; set; }
        public List<AlertaViewModel>? Alertas { get; set; }
    }
}
