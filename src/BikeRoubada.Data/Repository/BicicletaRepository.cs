using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BikeRoubada.Data.Repository
{
    public class BicicletaRepository : Repository<Bicicleta>, IBicicletaRepository
    {
        public BicicletaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Bicicleta> ObterBicicletaArquivos(Guid id)
        {
            return await Db.Bicicletas
                .AsNoTracking()
                .Include(e => e.Arquivos)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Bicicleta>> ObterBicicletasPorUsuario(Guid id)
        {
            return await Db.Bicicletas.AsNoTracking()
                //.Include(e => e.Usuario)
                .Include(e => e.Arquivos)
                .Include(e => e.Endereco)
                .Include(e => e.Roubos)
                .Where(e => e.Usuario.Id == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bicicleta>> ObterBicilcetasArquivos()
        {
            return await Db.Bicicletas
                .AsNoTracking()
                .Include(e => e.Arquivos)
                .ToListAsync();
        }
    }
}
