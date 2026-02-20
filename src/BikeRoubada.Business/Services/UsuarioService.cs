using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using BikeRoubada.Business.Validations;

namespace BikeRoubada.Business.Services
{
    public class UsuarioService : BaseService, IUsuarioService

    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task Adicionar(Usuario usuario)
        {
            if (_usuarioRepository.Buscar(u => u.IdentificadorPessoal != null &&  u.IdentificadorPessoal == usuario.IdentificadorPessoal).Result.Any())
            {
                Notificar("Já existe um usuário com o identificador pessoal fornecido");
            }

            if(_usuarioRepository.Buscar(u => u.Email == usuario.Email).Result.Any())
            {
                Notificar("Já existe um usuário com o email pessoal fornecido");
            }

            if (!ExecutarValidacao(new UsuarioValidation(), usuario))
            {
                return;
            }


            

            await _usuarioRepository.Adicionar(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {

            if (!_usuarioRepository.Buscar(u => u.IdentificadorPessoal == usuario.IdentificadorPessoal).Result.Any())
            {
                Notificar("Não existe um usuário com o identificador pessoal fornecido");
            }

            if (!ExecutarValidacao(new UsuarioValidation(), usuario))
            {
                return;
            }

            
            await _usuarioRepository.Atualizar(usuario);
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }

        public async Task Remover(Guid id)
        {
            if (_usuarioRepository.ObterPorId(id) == null)
            {
                Notificar("Não existe um usuário com o Id fornecido");
            }
            await _usuarioRepository.Remover(id);
        }
    }
}
