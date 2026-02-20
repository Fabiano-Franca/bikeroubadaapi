using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Data.Context;
using Microsoft.EntityFrameworkCore;

using NetTopologySuite.Geometries;

namespace BikeRoubada.Data.Repository
{
    public class RouboRepository : Repository<Roubo>, IRouboRepository
    {
        public RouboRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Roubo>> ObterRoubosPorRaio(Point point, int raio)
        {
            double raioGraus = raio / 111320.0;
            return await 
                Db.Roubos
                .AsNoTracking()
                .Where(e => e.Localizacao.IsWithinDistance(point, raioGraus))
                .ToListAsync();

        }

        public async Task<Roubo> ObterRouboComArquivos(Guid id)
        {
            Roubo roubo = await Db.Roubos
                .AsNoTracking()
                .Include(e => e.Arquivos)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            return roubo;
        }

        public Task<IEnumerable<Roubo>> ObterRoubosPorUsuario(Guid idUsuario)
        {
  
            throw new NotImplementedException();
        }

    }
}
