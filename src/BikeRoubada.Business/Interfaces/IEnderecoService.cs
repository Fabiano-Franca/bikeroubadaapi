using BikeRoubada.Business.Models;


namespace BikeRoubada.Business.Interfaces
{
    public interface IEnderecoService : IDisposable
    {
        Task<Endereco?> Adicionar(Endereco endereco);
        Task<Endereco?> Atualizar(Endereco endereco);
        Task Remover (Guid id);
    }
}
