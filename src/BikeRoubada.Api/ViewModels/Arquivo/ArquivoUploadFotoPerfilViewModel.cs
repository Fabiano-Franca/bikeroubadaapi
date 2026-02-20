using System.ComponentModel.DataAnnotations;

namespace BikeRoubada.Api.ViewModels.Arquivo
{
    public class ArquivoUploadFotoPerfilViewModel
    {
        [Required(ErrorMessage = "O Campo {0} é requerido")]
        public Guid IdUsuario { get; set; }
        [Required(ErrorMessage = "O Campo {0} é requerido")]
        public IFormFile FormContent { get; set; }
    }
}
