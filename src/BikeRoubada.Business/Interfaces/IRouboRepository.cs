using BikeRoubada.Business.Models;
using NetTopologySuite.Geometries;

namespace BikeRoubada.Business.Interfaces
{
    public interface IRouboRepository : IRepository<Roubo>
    {
        Task<IEnumerable<Roubo>> ObterRoubosPorUsuario(Guid idUsuario);
        Task<Roubo> ObterRouboComArquivos(Guid id);
        Task<IEnumerable<Roubo>> ObterRoubosPorRaio(Point point, int raio);
    }
}
