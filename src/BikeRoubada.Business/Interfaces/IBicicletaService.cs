using BikeRoubada.Business.Models;


namespace BikeRoubada.Business.Interfaces
{
    public interface IBicicletaService : IDisposable
    {
        Task<Bicicleta> Adicionar(Bicicleta bicicleta);
        Task Atualizar(Bicicleta bicicleta);
        Task Remover(Guid id);
    }
}
