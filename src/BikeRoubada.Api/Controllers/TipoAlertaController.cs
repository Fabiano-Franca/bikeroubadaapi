using AutoMapper;
using BikeRoubada.Api.ViewModels.TipoAlerta;
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeRoubada.Api.Controllers
{
    [Route("api/[controller]")]
    public class TipoAlertaController : MainController
    {
        private readonly ITipoAlertaService _tipoAlertaService;
        private readonly ITipoAlertaRepository _tipoAlertaRepository;
        private readonly IMapper _mapper;
        public TipoAlertaController(INotificador notificador, 
                                    ITipoAlertaRepository tipoAlertaRepository, 
                                    ITipoAlertaService tipoAlertaService,
                                    IMapper mapper
                                    ) : base(notificador)
        {
            _tipoAlertaRepository = tipoAlertaRepository;
            _tipoAlertaService = tipoAlertaService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAlertaApenasViewModel>>> ObterTodos()
        {
            return CustomResponse(HttpStatusCode.Created, _mapper.Map<IEnumerable<TipoAlertaApenasViewModel>>(await _tipoAlertaRepository.ObterTodos()));
        }

        [HttpGet("obter-por-id")]
        public async Task<ActionResult<TipoAlertaApenasViewModel>> ObterPorId(Guid id)
        {
            return CustomResponse(HttpStatusCode.OK, _mapper.Map<TipoAlertaApenasViewModel>(await _tipoAlertaRepository.Buscar(e => e.Id == id)));
        }


        [HttpPost]
        public async Task<ActionResult<TipoAlertaApenasViewModel>> Adicionar(TipoAlertaApenasViewModel tipoAlerta)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            await _tipoAlertaService.Adicionar(_mapper.Map<TipoAlerta>(tipoAlerta));
            return CustomResponse(HttpStatusCode.Created, tipoAlerta);
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(Guid id,  TipoAlertaApenasViewModel tipoAlertaApenasViewModel)
        {
            if(id != tipoAlertaApenasViewModel.Id)
            {
                NotificarErro("O parametro id é diferente do existente no objeto fornecido");
            }
            
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            await _tipoAlertaService.Atualizar(_mapper.Map<TipoAlerta>(tipoAlertaApenasViewModel));
            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            await _tipoAlertaService.Remover(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }


    }
}
