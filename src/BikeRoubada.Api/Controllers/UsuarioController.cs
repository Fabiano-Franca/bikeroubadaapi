using AutoMapper;
using BikeRoubada.Api.Utilities;
using BikeRoubada.Api.ViewModels.Arquivo;
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
        private readonly IFileHandler _fileHandler;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioRepository usuarioRepository, 
                                 IUsuarioService usuarioService, 
                                 IMapper mapper,
                                 IFileHandler fileHandler,
                                 INotificador notificador)  : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _fileHandler = fileHandler;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioApenasViewModel>>> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<UsuarioApenasViewModel>>(await _usuarioRepository.ObterTodos()));
        }

        [HttpGet("obter-por-id")]
        public async Task<ActionResult<UsuarioApenasViewModel>> ObterPorId(Guid id)
        {
            return CustomResponse(HttpStatusCode.OK, _mapper.Map<UsuarioApenasViewModel>(await _usuarioRepository.ObterPorId(id)));
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioApenasViewModel>> Adicionar(UsuarioApenasViewModel usuarioApenasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);

            }

            var usuario = _mapper.Map<Usuario>(usuarioApenasViewModel);

            await _usuarioService.Adicionar(usuario);
            usuarioApenasViewModel.Id = usuario.Id;
            return CustomResponse(HttpStatusCode.Created,  usuarioApenasViewModel);
        }

        [HttpPost("upload-foto-perfil")]
        public async Task<ActionResult> UploadFotoPerfil(ArquivoUploadFotoPerfilViewModel arquivoUploadViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            if (arquivoUploadViewModel.FormContent != null)
            {
                var fileResult = await _fileHandler.UploadFile(arquivoUploadViewModel.FormContent) as FileResult<string>;
                if (!fileResult.Succeeded)
                {
                    NotificarErro(fileResult.Error);
                    return CustomResponse();
                }

                var user = await _usuarioRepository.ObterPorId(arquivoUploadViewModel.IdUsuario);
                if (user != null) 
                {
                    user.FotoPerfil = fileResult.Content;
                }

                await _usuarioService.Atualizar(user);

                var result = await _fileHandler.DownloadFile(user.FotoPerfil) as FileResult<FileContent>;
                if (!result.Succeeded)
                {
                    NotificarErro(result.Error);
                    return CustomResponse();
                }

                return CustomResponse(HttpStatusCode.Created, new
                {
                    fileName = result.Content?.FileName,
                    type = result.Content?.Type
                });
                
                //return File(result.Content.Bytes, result.Content.Type, result.Content.FileName);
            }

            return CustomResponse();
        }

        [HttpGet("obter-foto-perfil")]
        public async Task<ActionResult> ObterFotoPerfil(string fileName)
        {

            var result = await _fileHandler.DownloadFile(fileName) as FileResult<FileContent>;
            if (!result.Succeeded)
            {
                NotificarErro(result.Error);
                return CustomResponse();
            }

            var file = File(result.Content.Bytes, result.Content.Type, result.Content.FileName);

            return file;
        }

        [HttpPut]
        public async Task<ActionResult<UsuarioApenasViewModel>> Atualizar(Guid id, [FromForm] UsuarioApenasViewModel usuarioApenasViewModel)
        {
            if (id != usuarioApenasViewModel.Id)
            {
                NotificarErro("O parâmetro id é diferente do existente no objeto fornecido");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            // 1. Mapeia e atualiza
            var usuario = _mapper.Map<Usuario>(usuarioApenasViewModel);
            await _usuarioService.Atualizar(usuario);

            // 2. Busca o usuário atualizado do banco para garantir integridade
            // (Ou você pode re-mapear o objeto 'usuario' se o Service já o preenche)
            var usuarioAtualizado = await _usuarioRepository.ObterPorId(id);

            // 3. Retorna o mapeamento para a ViewModel com Status 200 (OK)
            return CustomResponse(HttpStatusCode.OK, _mapper.Map<UsuarioApenasViewModel>(usuarioAtualizado));
        }

        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            await _usuarioService.Remover(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }

    }
}
