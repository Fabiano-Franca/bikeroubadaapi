using BikeRoubada.Business.Models;

namespace BikeRoubada.Business.Interfaces
{
     public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<IEnumerable<Bicicleta>> ObterBicicletasPorUsuario(Guid id);
        Task<IEnumerable<Endereco>> ObterEnderecosPorUsuario(Guid id);
        Task<Usuario> ObterUsuarioPorEmail(String email);
        Task<Usuario> ObterUsuarioCompletoPorEmail(String email);
    }
}
