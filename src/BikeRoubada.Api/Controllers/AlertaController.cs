using AutoMapper;
using BikeRoubada.Api.ViewModels;
using BikeRoubada.Api.ViewModels.Alerta;
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeRoubada.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlertaController : MainController
    {

        private readonly IAlertaRepository _alertaRepository;
        private readonly IAlertaService _alertaService;
        private readonly IMapper _mapper;
        public AlertaController(IAlertaRepository alertaRepository,
                                 IAlertaService alertaService,
                                 IMapper mapper,
                                 INotificador notificador) : base(notificador)
        {
            _alertaRepository = alertaRepository;
            _alertaService = alertaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaApenasViewModel>>> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<AlertaApenasViewModel>>(await _alertaRepository.ObterTodos()));
        }

        [HttpGet("obter-por-id")]
        public async Task<ActionResult<AlertaApenasViewModel>> ObterPorId(Guid id)
        {
            return CustomResponse(HttpStatusCode.OK, _mapper.Map<AlertaApenasViewModel>(await _alertaRepository.ObterPorId(id)));
        }

        [HttpGet("obter-por-id-usuario")]
        public async Task<ActionResult<IEnumerable<AlertaApenasViewModel>>> ObterPorUsuario(Guid id)
        {
            return Ok(_mapper.Map<IEnumerable<AlertaApenasViewModel>>(await _alertaRepository.Buscar(e => e.IdUsuarioGerador == id)));
        }

        [HttpPost]
        public async Task<ActionResult<AlertaApenasViewModel>> Adicionar(AlertaApenasViewModel alertaApenasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);

            }


            var alerta = _mapper.Map<Alerta>(alertaApenasViewModel);
            await _alertaService.Adicionar(alerta);
            alertaApenasViewModel.Id = alerta.Id;
            return CustomResponse(HttpStatusCode.Created, alertaApenasViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(Guid id,  AlertaApenasViewModel alertaApenasViewModel)
        {
            if(id != alertaApenasViewModel.Id)
            {
                NotificarErro("O parametro id é diferente do existente no objeto fornecido");
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            await _alertaService.Atualizar(_mapper.Map<Alerta>(alertaApenasViewModel));
            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            await _alertaService.Remover(id);
            return CustomResponse(HttpStatusCode.NoContent);

        }



    }
}
