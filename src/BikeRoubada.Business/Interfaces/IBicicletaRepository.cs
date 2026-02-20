using BikeRoubada.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRoubada.Business.Interfaces
{
    public interface IBicicletaRepository : IRepository<Bicicleta>
    {
        Task<IEnumerable<Bicicleta>> ObterBicicletasPorUsuario(Guid id);
        Task<IEnumerable<Bicicleta>> ObterBicilcetasArquivos();
        Task<Bicicleta> ObterBicicletaArquivos(Guid id);
    }
}
