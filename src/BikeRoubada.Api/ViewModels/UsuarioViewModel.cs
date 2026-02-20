using System.ComponentModel.DataAnnotations;

using System.Globalization;
namespace BikeRoubada.Api.ViewModels
{
    public class UsuarioViewModel 
    {


        Guid Id { get; set; }
        public string Nome { get; set; }
        public string? FotoPerfil { get; set; }
        public string IdentificadorPessoal { get; set; }
        public string? Genero { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<EnderecoViewModel> Enderecos { get; set; }
        public List<BicicletaViewModel> Bicicletas { get; set; }
        public int TipoPessoa { get; set; }

        

    }
}
