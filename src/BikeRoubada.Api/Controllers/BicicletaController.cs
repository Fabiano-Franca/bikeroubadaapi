using Microsoft.AspNetCore.Mvc;
using BikeRoubada.Api.ViewModels.Bicicleta;
using BikeRoubada.Business.Interfaces;
using AutoMapper;
using BikeRoubada.Business.Models;
using System.Net;
using BikeRoubada.Api.ViewModels;

namespace BikeRoubada.Api.Controllers
{
    [Route("api/[controller]")]
    public class BicicletaController : MainController
    {
        private readonly IBicicletaRepository _bicicletaRepository;
        private readonly IBicicletaService _bicicletaService;
        private readonly IMapper _mapper;

        public BicicletaController(INotificador notificador,
                                   IBicicletaRepository bicicletaRepository,
                                   IBicicletaService bicicletaService,
                                   IMapper mapper) : base(notificador)
        {
            _bicicletaRepository = bicicletaRepository;
            _bicicletaService = bicicletaService;
            _mapper = mapper;;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BicicletaApenasViewModel>>> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<BicicletaApenasViewModel>>(await _bicicletaRepository.ObterTodos()));
        }

        [HttpGet("obter-por-id")]
        public async Task<ActionResult<BicicletaApenasViewModel>> ObterPorId(Guid id)
        {
            return Ok(_mapper.Map<BicicletaApenasViewModel>(await _bicicletaRepository.ObterPorId(id)));
        }

        [HttpGet("Obter-bicicletas-por-usuario")]
        public async Task<ActionResult> ObterBicicletasPorUsuario(Guid id)
        {
            
            return CustomResponse(HttpStatusCode.OK, new
            {
                bikes = _mapper.Map<IEnumerable<BicicletaViewModel>>(await _bicicletaRepository.ObterBicicletasPorUsuario(id))
            });
        }

        [HttpGet("obter-por-id-usuario")]
        public async Task<ActionResult<IEnumerable<BicicletaApenasViewModel>>> ObterPorIdUsuario(Guid id)
        {
            return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<BicicletaApenasViewModel>>(await _bicicletaRepository.Buscar(e => e.IdUsuario == id)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(BicicletaApenasViewModel bicicletaApenasViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            // Mapeie apenas uma vez e use essa variável
            var bicicletaParaAdicionar = _mapper.Map<Bicicleta>(bicicletaApenasViewModel);
            var bikeCadastrada = await _bicicletaService.Adicionar(bicicletaParaAdicionar);

            // Verifique se o CustomResponse não está falhando ao tentar serializar a 'bikeCadastrada'
            return CustomResponse(System.Net.HttpStatusCode.Created, new { bikeCadastrada } );
            //return CustomResponse(HttpStatusCode.Created, new { id = bikeCadastradaViewModel.Id });
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(Guid id,  BicicletaApenasViewModel bicicletaApenasViewModel)
        {
            if(id != bicicletaApenasViewModel.Id)
            {
                NotificarErro("O id fornecido é diferente do id existente no objeto");
                return CustomResponse();
            }

            await _bicicletaService.Atualizar(_mapper.Map<Bicicleta>(bicicletaApenasViewModel));
            return CustomResponse(System.Net.HttpStatusCode.Created, new { bicicletaApenasViewModel });

        }


        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            await _bicicletaRepository.Remover(id);
            return NoContent();
        }
    }

   
}
