

using NetTopologySuite.Geometries;

namespace BikeRoubada.Business.Models
{
    public class Alerta : Entity
    {
        public Guid IdBicicleta { get; set; }
        public Bicicleta? Bicicleta { get; set; }
        public Point Localizacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public Usuario? UsuarioGerador { get; set; }
        public Guid IdTipoAlerta { get; set; }
        public TipoAlerta? TipoAlerta { get; set; }
    }
}
