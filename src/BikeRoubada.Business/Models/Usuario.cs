using System.ComponentModel.DataAnnotations;

using System.Globalization;
namespace BikeRoubada.Business.Models
{
    public class Usuario : Entity
    {
        public string Nome { get; set; }
        public string? FotoPerfil { get; set; }
        public string IdentificadorPessoal { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string? Genero { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<Endereco> Enderecos { get; set; }

        public List<Bicicleta> Bicicletas { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
    }
}
