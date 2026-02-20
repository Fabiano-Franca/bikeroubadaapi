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
    public class EnderecoService : BaseService, IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoService(IEnderecoRepository enderecoRepository, INotificador notificador) : base(notificador)
        {
            _enderecoRepository = enderecoRepository;
        }
        public async Task<Endereco?> Adicionar(Endereco endereco)
        {
            endereco.Cep = endereco.Cep?.Replace("-", "").Trim();

            if (!ExecutarValidacao(new EnderecoValidation(), endereco))
            {
                return null;
            }
            if (endereco.Principal)
            {
                await TrocarEnderecoPrincipal(endereco.IdUsuario);
            }

            return await _enderecoRepository.Adicionar(endereco);
        }

        public async Task<Endereco?> Atualizar(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco))
            {
                return null;
            }

            if (endereco.Principal)
            {
                await TrocarEnderecoPrincipal(endereco.IdUsuario);
            }

            return await _enderecoRepository.Atualizar(endereco);
        }

        public void Dispose()
        {
            _enderecoRepository?.Dispose();
        }

        public async Task Remover(Guid id)
        {
           await _enderecoRepository.Remover(id);
        }

        private async Task TrocarEnderecoPrincipal(Guid? IdUsuario)
        {
            var enderecos = await _enderecoRepository.Buscar(e => e.Principal && e.IdUsuario == IdUsuario);
            if (enderecos.Any())
            {
                foreach (var item in enderecos)
                {
                    item.Principal = false;
                    await _enderecoRepository.Atualizar(item);

                }
            }
        }
    }
}
