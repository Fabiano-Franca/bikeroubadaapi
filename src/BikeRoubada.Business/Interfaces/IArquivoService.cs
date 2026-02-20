using BikeRoubada.Business.Models;

namespace BikeRoubada.Business.Interfaces
{
    public interface IArquivoService : IDisposable
    {
        Task Adicionar(Arquivo arquivo);
        Task Atualizar(Arquivo arquivo);
        Task Remover(Guid id);
    }
}
