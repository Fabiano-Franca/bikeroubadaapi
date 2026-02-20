using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace BikeRoubada.Business.Models
{
    public class Bicicleta : Entity
    {
        public string? Serial { get; set; }
        public string? Descricao { get; set; }
        public string? Detalhes { get; set; }
        public Point LocalizacaoCadastro { get; set;}
        public Guid IdEndereco { get; set; }
        public Guid IdUsuario { get; set; }
        public Endereco? Endereco { get; set; }
        public Usuario Usuario { get; set; }
        public List<Roubo>? Roubos { get; set; }
        public List<Arquivo>? Arquivos { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
