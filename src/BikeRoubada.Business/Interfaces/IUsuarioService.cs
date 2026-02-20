using BikeRoubada.Business.Models;


namespace BikeRoubada.Business.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task Remover(Guid id);
    }
}
