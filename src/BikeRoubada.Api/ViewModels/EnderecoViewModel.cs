using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BikeRoubada.Api.ViewModels
{
    public class EnderecoViewModel
    {

        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Rua { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Numero { get; set; }
        public string? Complemento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Estado { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Cep { get; set; }
        public bool Principal {  get; set; }
  
        public Guid? IdUsuario { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public List<BicicletaViewModel>? Bicicletas { get; set; }
        public DateTime DataCadastro { get; set; }


    }
}
