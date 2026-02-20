using BikeRoubada.Business.Models;

namespace BikeRoubada.Business.Interfaces
{
    public interface ITipoAlertaService
    {
        Task Adicionar(TipoAlerta tipoAlerta);
        Task Atualizar(TipoAlerta tipoAlerta);
        Task Remover(Guid id);
    }
}
