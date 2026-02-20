
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Business.Validations;

namespace BikeRoubada.Business.Services
{
    public class RouboService : BaseService, IRouboService
    {
        private readonly IRouboRepository _rouboRepository;
        
        public RouboService(IRouboRepository rouboRepository, INotificador notificador) : base(notificador)
        {
            _rouboRepository = rouboRepository; 
        }
        public async Task Adicionar(Roubo roubo)
        {
            if(!ExecutarValidacao(new RouboValidation(), roubo))
            {
                return;
            }
            await _rouboRepository.Adicionar(roubo);
        }

        public async Task Atualizar(Roubo roubo)
        {
            if (!ExecutarValidacao(new RouboValidation(), roubo))
            {
                return;
            }
            await _rouboRepository.Atualizar(roubo);
        }

        public void Dispose()
        {
            _rouboRepository?.Dispose();
        }

        public async Task Remover(Guid id)
        {
            await _rouboRepository.Remover(id);
        }
    }
}
