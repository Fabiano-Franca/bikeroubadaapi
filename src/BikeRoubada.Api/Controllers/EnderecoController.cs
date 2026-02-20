using AutoMapper;
using BikeRoubada.Api.ViewModels.Endereco;
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeRoubada.Api.Controllers
{
    [Route("api/[controller]")]
    public class EnderecoController : MainController
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecoController(IEnderecoRepository enderecoRepository, 
                                  IEnderecoService enderecoService, 
                                  IMapper mapper,
                                  INotificador notificador) : base(notificador)
        {
            _enderecoRepository = enderecoRepository;
            _enderecoService = enderecoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoApenasViewModel>>> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<EnderecoApenasViewModel>>(await _enderecoRepository.ObterTodos()));
        }

        [HttpGet("obter-por-id")]
        public async Task<ActionResult<EnderecoApenasViewModel>> ObterPorId(Guid id)
        {
            return CustomResponse(HttpStatusCode.OK, _mapper.Map<EnderecoApenasViewModel>(await _enderecoRepository.ObterPorId(id)));
        }

        [HttpGet("obter-por-id-usuario")]
        public async Task<ActionResult<IEnumerable<EnderecoApenasViewModel>>> ObterPorIdUsuario(Guid id)
        {
            return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<EnderecoApenasViewModel>>(await _enderecoRepository.Buscar(e => e.IdUsuario == id)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(EnderecoApenasViewModel enderecoApenasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var endereco = await _enderecoService.Adicionar(_mapper.Map<Endereco>(enderecoApenasViewModel));

            return CustomResponse(System.Net.HttpStatusCode.Created, new { endereco });
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(Guid id, EnderecoApenasViewModel enderecoApenasViewModel)
        {
            if(id != enderecoApenasViewModel.Id)
            {
                NotificarErro("O parametro id é diferente do existente no objeto fornecido");
            }
            
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var endereco = await _enderecoService.Atualizar(_mapper.Map<Endereco>(enderecoApenasViewModel));

            return CustomResponse(System.Net.HttpStatusCode.Created, new { endereco });
        }

        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
           await _enderecoService.Remover(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }

        
    }
}
