using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace BikeRoubada.Business.Models
{
    public class Endereco : Entity
    {

        public string? Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string? Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string? Cep { get; set; }
        public bool Principal {  get; set; }
        public Guid? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public List<Bicicleta>? Bicicletas { get; set; }
        public DateTime DataCadastro { get; set; }


    }
}
