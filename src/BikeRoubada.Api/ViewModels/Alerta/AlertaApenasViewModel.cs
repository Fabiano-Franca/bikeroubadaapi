using BikeRoubada.Api.AuxiliaryModels;
using NetTopologySuite.Geometries;

namespace BikeRoubada.Api.ViewModels.Alerta
{
    public class AlertaApenasViewModel
    {
        public Guid Id { get; set; }
        public Guid IdBicicleta { get; set; }
        public SimplePoint Localizacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public Guid IdTipoAlerta { get; set; }
    }
}
