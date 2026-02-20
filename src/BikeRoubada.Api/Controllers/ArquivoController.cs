using AutoMapper;
using BikeRoubada.Api.Utilities;
using BikeRoubada.Api.ViewModels.Arquivo;
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeRoubada.Api.Controllers
{
    [Route("api/[controller]")]
    public class ArquivoController : MainController
    {
        private readonly IArquivoService _arquivoService;
        private readonly IArquivoRepository _arquivoRepository;
        private readonly IFileHandler _fileHandler;
        private readonly IMapper _mapper;

        public ArquivoController(INotificador notificador,
                                 IFileHandler fileHandler,
                                 IArquivoService arquivoService,
                                 IArquivoRepository arquivoRepository,
                                 IMapper mapper) : base(notificador)
        {
            _arquivoService = arquivoService;
            _arquivoRepository = arquivoRepository;
            _fileHandler = fileHandler;
            _mapper = mapper;
        }

        [RequestSizeLimit(100_000_000)]
        [HttpPost]
        public async Task<ActionResult<ArquivoApenasViewModel>> Adicionar([FromForm] ArquivoUploadViewModel arquivoUploadViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            ArquivoApenasViewModel arquivoApenasViewModel = null;

            foreach (var file in arquivoUploadViewModel.FormContent)
            {
                var fileResult = await _fileHandler.UploadFile(file) as FileResult<string>;
                if (!fileResult.Succeeded)
                {
                    NotificarErro(fileResult.Error);
                    return CustomResponse();
                }

                arquivoApenasViewModel = new ArquivoApenasViewModel()
                {
                    NomeArquivo = fileResult.Content,

                };

                if (arquivoUploadViewModel.NomeEntidade.ToLower() == "roubo")
                {
                    arquivoApenasViewModel.IdRoubo = arquivoUploadViewModel.IdEntidade;
                    arquivoApenasViewModel.Tipo = arquivoUploadViewModel.Tipo;
                }

                if (arquivoUploadViewModel.NomeEntidade.ToLower() == "bicicleta")
                {
                    arquivoApenasViewModel.IdBicicleta = arquivoUploadViewModel.IdEntidade;
                    arquivoApenasViewModel.Tipo = arquivoUploadViewModel.Tipo;
                }
                var arquivo = _mapper.Map<Arquivo>(arquivoApenasViewModel);

                await _arquivoService.Adicionar(arquivo);
                arquivoApenasViewModel.Id = arquivo.Id;
                arquivoApenasViewModel.DataCadastro = arquivo.DataCadastro;
            }

            return CustomResponse(HttpStatusCode.Created, arquivoApenasViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArquivoApenasViewModel>>> ObterTodos()
        {
            
            return CustomResponse(HttpStatusCode.OK,  _mapper.Map<IEnumerable<ArquivoApenasViewModel>>(await _arquivoRepository.ObterTodos()));
        }

        [HttpGet("obter-arquivo")]
        public async Task<IActionResult> ObterArquivo(Guid id)
        {
            Arquivo? arquivo = await _arquivoRepository.ObterPorId(id);
            if(arquivo == null)
            {
                NotificarErro("O id fornecido não existe no banco de dados");
                return CustomResponse();
            }

            var result = await _fileHandler.DownloadFile(arquivo.NomeArquivo) as FileResult<FileContent>;
            if (!result.Succeeded)
            {
                NotificarErro(result.Error);
                return CustomResponse();
            }

            return File(result.Content.Bytes, result.Content.Type, result.Content.FileName);
        }

        [HttpGet("exibir-imagem")]
        public Microsoft.AspNetCore.Mvc.FileResult ImagemGet(string fileName)
        {
            string arquivo = _fileHandler.GetPathFile(fileName);
            string extensao = Path.GetExtension(arquivo);

            try
            {
                byte[] data = System.IO.File.ReadAllBytes(arquivo);
                return File(data, "image/" + extensao,fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            var arquivo = await _arquivoRepository.ObterPorId(id);
            if(arquivo == null)
            {
                NotificarErro("O id fornecido não existe no banco de dados");
                return CustomResponse();
            }

            var fileResult = _fileHandler.DeleteFile(arquivo.NomeArquivo);
            if (!fileResult.Succeeded)
            {
                NotificarErro(fileResult.Error);
            }

            await _arquivoService.Remover(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }

    }
}
