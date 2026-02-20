using NetTopologySuite.Geometries;

namespace BikeRoubada.Business.Models
{ 
    public class Roubo : Entity
    {
        public string Relato { get; set; }
        public DateTime DataRoubo {  get; set; }
        public DateTime? DataRecuperacao { get; set; }
        public  Point Localizacao { get; set; }
        public string? NumeroBoletim {  get; set; }
        public Guid IdBicicleta { get; set; }
        public Bicicleta Bicicleta { get; set; }
        public List<Arquivo> Arquivos { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
