using BikeRoubada.Business.Models;
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BikeRoubada.Data.Repository
{
    public class AlertaRepository : Repository<Alerta>, IAlertaRepository
    {
        public AlertaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
