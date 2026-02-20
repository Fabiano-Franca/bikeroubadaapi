

using NetTopologySuite.Geometries;

namespace BikeRoubada.Api.ViewModels
{
    public class AlertaViewModel 
    {
        Guid Id { get; set; }
        public Guid IdBicicleta { get; set; }
        public Point Localizacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public UsuarioViewModel? UsuarioGerador { get; set; }
        public Guid IdTipoAlerta { get; set; }
        public TipoAlertaViewModel? TipoAlerta { get; set; }
    }
}
