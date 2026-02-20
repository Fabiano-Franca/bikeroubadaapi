using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BikeRoubada.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Bicicleta>> ObterBicicletasPorUsuario(Guid id)
        {
            return await Db.Bicicletas.Where(e => e.IdUsuario == id).ToListAsync();
        }



        public async Task<IEnumerable<Endereco>> ObterEnderecosPorUsuario(Guid id)
        {
            return await Db.Enderecos.Where(e => e.IdUsuario == id).ToListAsync();
        }

        public async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            return await Db.Usuarios
                .Include(u => u.Enderecos)
                .Include(u => u.Bicicletas)
                .ThenInclude(b => b.Arquivos)
                .Where(u => u.Email == email)
                .FirstAsync();
        }

        public async Task<Usuario> ObterUsuarioCompletoPorEmail(string email)
        {
            return await Db.Usuarios.Where(u => u.Email == email).FirstAsync();
        }
    }
}
