using System.ComponentModel.DataAnnotations;

namespace BikeRoubada.Api.ViewModels.TipoAlerta
{
    public class TipoAlertaApenasViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(30, ErrorMessage = "O campo {0} pode conter no máximo {1} caracteres")]
        public string? Nome { get; set; }
    }
}
