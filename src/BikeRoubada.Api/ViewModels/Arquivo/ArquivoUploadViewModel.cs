using BikeRoubada.Business.Enums;
using System.ComponentModel.DataAnnotations;

namespace BikeRoubada.Api.ViewModels.Arquivo
{
    public class ArquivoUploadViewModel
    {
        [Required(ErrorMessage = "O Campo {0} é requerido")]
        public Guid IdEntidade { get; set; }
        [Required(ErrorMessage = "O Campo {0} é requerido")]
        public string NomeEntidade { get; set; }
        [Required(ErrorMessage = "O Campo {0} é requerido")]
        public TipoArquivo Tipo { get; set; }
        public bool Destaque { get; set; }
        [Required(ErrorMessage = "O Campo {0} é requerido")]
        public List<IFormFile> FormContent { get; set; }
    }
}
