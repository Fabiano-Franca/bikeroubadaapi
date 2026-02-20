using BikeRoubada.Business.Models;

namespace BikeRoubada.Business.Interfaces
{
    public interface IAlertaService : IDisposable
    {
        Task Adicionar(Alerta alerta);
        Task Atualizar(Alerta alerta);
        Task Remover(Guid id);
    }
}
