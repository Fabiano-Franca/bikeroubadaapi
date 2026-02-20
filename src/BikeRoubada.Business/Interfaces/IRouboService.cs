using BikeRoubada.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRoubada.Business.Interfaces
{
    public interface IRouboService : IDisposable
    {
        Task Adicionar(Roubo roubo);
        Task Atualizar(Roubo roubo);
        Task Remover(Guid id);
    }
}
