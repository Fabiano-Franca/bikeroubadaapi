namespace BikeRoubada.Business.Models
{
    public class TipoAlerta : Entity
    {
        public string? Nome { get; set; }
        public List<Alerta>? Alertas { get; set; }
    }
}
