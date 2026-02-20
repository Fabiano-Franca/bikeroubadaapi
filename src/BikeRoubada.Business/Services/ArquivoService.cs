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
    public class ArquivoService : BaseService, IArquivoService
    {
        private readonly IArquivoRepository _arquivoRepository;
        public ArquivoService(IArquivoRepository arquivoRepository, INotificador notificador) : base(notificador) 
        {
            _arquivoRepository = arquivoRepository;
        }

        public async Task Adicionar(Arquivo arquivo)
        {
            if (!ExecutarValidacao(new ArquivoValidation(),arquivo))
            {
                return;
            }
            await _arquivoRepository.Adicionar(arquivo);
        }

        public async Task Atualizar(Arquivo arquivo)
        {
            if (!ExecutarValidacao(new ArquivoValidation(), arquivo))
            {
                return;
            }
            await _arquivoRepository.Atualizar(arquivo);
        }

        public void Dispose()
        {
            _arquivoRepository?.Dispose();
        }

        public async Task Remover(Guid id)
        {
            await _arquivoRepository.Remover(id);
        }
    }
}
