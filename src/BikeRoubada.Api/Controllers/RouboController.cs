using AutoMapper;
using BikeRoubada.Api.ViewModels;
using BikeRoubada.Api.ViewModels.Roubo;
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeRoubada.Api.Controllers
{
    [Route("api/[controller]")]
    public class RouboController : MainController
    {
        private readonly IRouboService _rouboService;
        private readonly IRouboRepository _rouboRepository;
        private readonly IMapper _mapper;
        
        public RouboController(INotificador notificador,
                               IRouboService rouboService,
                               IRouboRepository rouboRepository,
                               IMapper mapper) : base(notificador) 
        {
            _rouboService = rouboService;
            _rouboRepository = rouboRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouboApenasViewModel>>> ObterTodos()
        {
            return CustomResponse(HttpStatusCode.OK, _mapper.Map<IEnumerable<RouboApenasViewModel>>(await _rouboRepository.ObterTodos()));
        }

        [HttpGet("obter-por-id")]
        public async Task<ActionResult<RouboViewModel>> ObterPorId(Guid id)
        {
            var roubo = _mapper.Map<RouboViewModel>(await _rouboRepository.ObterRouboComArquivos(id));
            return CustomResponse(HttpStatusCode.OK, roubo);
        }
        
        [HttpPost]
        public async Task<ActionResult<RouboApenasViewModel>> Adicionar(RouboApenasViewModel rouboApenasViewModel)
        {
            if(!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var roubo = _mapper.Map<Roubo>(rouboApenasViewModel);
            await _rouboService.Adicionar(roubo);
            rouboApenasViewModel.Id = roubo.Id;
            return CustomResponse(HttpStatusCode.OK, rouboApenasViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(Guid id, RouboApenasViewModel rouboApenasViewModel)
        {
            if(id != rouboApenasViewModel.Id)
            {
                NotificarErro("O parametro id é diferente do existente no objeto fornecido");
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            var roubo = _mapper.Map<Roubo>(rouboApenasViewModel);

            await _rouboService.Atualizar(roubo);

            return CustomResponse(HttpStatusCode.OK, rouboApenasViewModel);
        }

        [HttpDelete]
        public async Task<ActionResult> Remover(Guid id)
        {
            await _rouboService.Remover(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
