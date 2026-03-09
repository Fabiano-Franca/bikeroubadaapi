using AutoMapper;
using BikeRoubada.Api.ViewModels.Usuario;
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeRoubada.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository,
                                 IUsuarioService usuarioService,
                                 IMapper mapper,
                                 INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        // 1. Ajustado para retornar a lista com as strings Base64 que já estão no banco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioApenasViewModel>>> ObterTodos()
        {
            var usuarios = await _usuarioRepository.ObterTodos();

            // O mapeamento automático já deve levar a string de usuario.FotoPerfil 
            // para a propriedade correspondente na ViewModel
            var viewModels = _mapper.Map<IEnumerable<UsuarioApenasViewModel>>(usuarios);

            return Ok(viewModels);
        }

        // 2. Ajustado para retornar o usuário com a foto em Base64 direto do banco
        [HttpGet("obter-por-id")]
        public async Task<ActionResult<UsuarioApenasViewModel>> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            if (usuario == null) return NotFound();

            var viewModel = _mapper.Map<UsuarioApenasViewModel>(usuario);

            // NÃO PRECISA MAIS DE PreencherFotoPerfilBase64, pois a string 
            // já está na propriedade FotoPerfil vinda do banco.

            return CustomResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost("upload-foto-perfil")]
        public async Task<ActionResult> UploadFotoPerfil([FromForm] Guid idUsuario, [FromForm] IFormFile FormContent)
        {
            if (FormContent == null || FormContent.Length == 0)
            {
                NotificarErro("Nenhum arquivo de imagem foi enviado.");
                return CustomResponse();
            }

            var usuario = await _usuarioRepository.ObterPorId(idUsuario);
            if (usuario == null)
            {
                NotificarErro("Utilizador não encontrado.");
                return CustomResponse(HttpStatusCode.NotFound);
            }

            // CONVERSÃO E PERSISTÊNCIA NO BANCO
            using (var ms = new MemoryStream())
            {
                await FormContent.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();

                var tipoArquivo = FormContent.ContentType;
                // Salva a string formatada direto na coluna do banco
                usuario.FotoPerfil = $"data:{tipoArquivo};base64,{Convert.ToBase64String(fileBytes)}";
            }

            await _usuarioService.Atualizar(usuario);

            return CustomResponse(HttpStatusCode.OK, new { fotoPerfil = usuario.FotoPerfil });
        }

        // 3. REMOVIDO OU COMENTADO: ObterFotoPerfil (pois não há mais arquivos físicos)
        // Se o App precisar de um endpoint que retorne apenas a string:
        [HttpGet("obter-string-foto")]
        public async Task<IActionResult> ObterStringFoto(Guid idUsuario)
        {
            var user = await _usuarioRepository.ObterPorId(idUsuario);
            if (user == null || string.IsNullOrEmpty(user.FotoPerfil))
                return NotFound();

            return Ok(new { fotoBase64 = user.FotoPerfil });
        }

        //[HttpPut]
        //public async Task<ActionResult<UsuarioApenasViewModel>> Atualizar2(Guid id, [FromForm] UsuarioApenasViewModel usuarioApenasViewModel)
        //{
        //    if (id != usuarioApenasViewModel.Id)
        //    {
        //        NotificarErro("O parâmetro id é diferente do objeto fornecido");
        //        return CustomResponse();
        //    }

        //    if (!ModelState.IsValid) return CustomResponse(ModelState);

        //    var usuario = _mapper.Map<Usuario>(usuarioApenasViewModel);
        //    await _usuarioService.Atualizar(usuario);

        //    var usuarioAtualizado = await _usuarioRepository.ObterPorId(id);
        //    return CustomResponse(HttpStatusCode.OK, _mapper.Map<UsuarioApenasViewModel>(usuarioAtualizado));
        //}

        //[HttpPut]
        //public async Task<ActionResult<UsuarioApenasViewModel>> Atualizar2(Guid id, [FromForm] UsuarioApenasViewModel usuarioApenasViewModel)
        //{
        //    if (id != usuarioApenasViewModel.Id)
        //    {
        //        NotificarErro("O parâmetro id é diferente do objeto fornecido");
        //        return CustomResponse();
        //    }

        //    if (!ModelState.IsValid) return CustomResponse(ModelState);

        //    // 1. Busca o usuário atual no banco de dados para não perder a foto
        //    var usuarioBanco = await _usuarioRepository.ObterPorId(id);
        //    if (usuarioBanco == null) return NotFound();

        //    // 2. Atualiza apenas os campos de texto vindos da ViewModel
        //    usuarioBanco.Nome = usuarioApenasViewModel.Nome;
        //    usuarioBanco.Email = usuarioApenasViewModel.Email;
        //    usuarioBanco.Telefone = usuarioApenasViewModel.Telefone;
        //    usuarioBanco.IdentificadorPessoal = usuarioApenasViewModel.IdentificadorPessoal;
        //    usuarioBanco.Genero = usuarioApenasViewModel.Genero;
        //    usuarioBanco.TipoPessoa = (TipoPessoa)usuarioApenasViewModel.TipoPessoa;

        //    // A FotoPerfil permanece a que já estava no 'usuarioBanco'
        //    await _usuarioService.Atualizar(usuarioBanco);

        //    return CustomResponse(HttpStatusCode.OK, _mapper.Map<UsuarioApenasViewModel>(usuarioBanco));
        //}

        [HttpPut]
        public async Task<ActionResult<UsuarioApenasViewModel>> Atualizar(Guid id, [FromForm] UsuarioApenasViewModel usuarioApenasViewModel)
        {
            if (id != usuarioApenasViewModel.Id)
            {
                NotificarErro("O parâmetro id é diferente do objeto fornecido");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            // 1. Busca o usuário atual no banco de dados para não perder a foto
            var usuarioBanco = await _usuarioRepository.ObterPorId(id);
            if (usuarioBanco == null) return NotFound();

            // 2. Atualiza apenas os campos de texto vindos da ViewModel
            usuarioBanco.Nome = usuarioApenasViewModel.Nome;
            usuarioBanco.Email = usuarioApenasViewModel.Email;
            usuarioBanco.Telefone = usuarioApenasViewModel.Telefone;
            usuarioBanco.IdentificadorPessoal = usuarioApenasViewModel.IdentificadorPessoal;
            usuarioBanco.Genero = usuarioApenasViewModel.Genero;
            usuarioBanco.TipoPessoa = (TipoPessoa)usuarioApenasViewModel.TipoPessoa;

            // A FotoPerfil permanece a que já estava no 'usuarioBanco'
            await _usuarioService.Atualizar(usuarioBanco);

            return CustomResponse(HttpStatusCode.OK, _mapper.Map<UsuarioApenasViewModel>(usuarioBanco));
        }

        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            await _usuarioService.Remover(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}