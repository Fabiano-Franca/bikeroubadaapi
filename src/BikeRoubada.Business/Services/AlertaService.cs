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
    public class AlertaService : BaseService, IAlertaService
    {
        
        private readonly IAlertaRepository _alertaRepository;
        public AlertaService(IAlertaRepository alertaRepository, INotificador notificador) : base(notificador) 
        {
            _alertaRepository = alertaRepository;
        }
        public async Task Adicionar(Alerta alerta)
        {

            if (!ExecutarValidacao(new AlertaValidation(), alerta))
            {
                return;
            }
            await _alertaRepository.Adicionar(alerta);
        }

        public async Task Atualizar(Alerta alerta)
        {
            if (!ExecutarValidacao(new AlertaValidation(), alerta))
            {
                return;
            }
            await _alertaRepository.Atualizar(alerta);
        }

        public void Dispose()
        {
            _alertaRepository.Dispose();
        }

        public async Task Remover(Guid id)
        {
            await _alertaRepository.Remover(id);
        }
    }
}
