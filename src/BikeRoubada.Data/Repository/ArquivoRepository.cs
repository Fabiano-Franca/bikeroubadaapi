using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BikeRoubada.Data.Repository
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
