using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Business.Validations;

namespace BikeRoubada.Business.Services
{
    public class BicicletaService : BaseService, IBicicletaService
    {
        private readonly IBicicletaRepository _bicicletaRepository;


        public BicicletaService(IBicicletaRepository bicicletaRepository, INotificador notificador) : base(notificador)
        {
            _bicicletaRepository = bicicletaRepository;
        }

        public async Task<Bicicleta> Adicionar(Bicicleta bicicleta)
        {
            if (!ExecutarValidacao(new BicicletaValidation(), bicicleta))
            {
                Notificar("Objeto inválido.");
            }

            //if(await _bicicletaRepository.ObterPorId(bicicleta.Id) == null)
            //{
            //    Notificar("Não existe registro com o id fornecido");
            //}
            return await _bicicletaRepository.Adicionar(bicicleta);
        }

        public async Task Atualizar(Bicicleta bicicleta)
        {
            if (!ExecutarValidacao(new BicicletaValidation(), bicicleta))
            {
                return;
            }
            await _bicicletaRepository.Atualizar(bicicleta);  
        }

        public void Dispose()
        {
            _bicicletaRepository.Dispose();
        }

        public async Task Remover(Guid id)
        {
            await _bicicletaRepository.Remover(id);
        }
    }
}
