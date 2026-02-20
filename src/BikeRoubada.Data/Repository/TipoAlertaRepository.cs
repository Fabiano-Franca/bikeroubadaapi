using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BikeRoubada.Data.Repository
{
    public class TipoAlertaRepository : Repository<TipoAlerta>, ITipoAlertaRepository
    {
        public TipoAlertaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
