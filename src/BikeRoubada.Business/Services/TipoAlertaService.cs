using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Business.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRoubada.Business.Services
{
    public class TipoAlertaService : BaseService, ITipoAlertaService
    {
        private readonly ITipoAlertaRepository _tipoAlertaRepository;  
        public TipoAlertaService(ITipoAlertaRepository tipoAlertaRepository, INotificador notificador) : base(notificador)
        {
            _tipoAlertaRepository = tipoAlertaRepository;
        }
        public async Task Adicionar(TipoAlerta tipoAlerta)
        {
            if(!ExecutarValidacao(new TipoAlertaValidation(), tipoAlerta))
            {
                return;
            }
            await _tipoAlertaRepository.Adicionar(tipoAlerta);
        }

        public async Task Atualizar(TipoAlerta tipoAlerta)
        {
            if (!ExecutarValidacao(new TipoAlertaValidation(), tipoAlerta))
            {
                return;
            }
            await _tipoAlertaRepository.Atualizar(tipoAlerta);
        }

        public async Task Remover(Guid id)
        {
            await _tipoAlertaRepository.Remover(id);
        }

        public void Dispose()
        {
            _tipoAlertaRepository.Dispose();
        }
    }
}
