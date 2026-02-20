using AutoMapper;
using BikeRoubada.Api.AuxiliaryModels;
using BikeRoubada.Api.ViewModels;
using BikeRoubada.Api.ViewModels.Alerta;
using BikeRoubada.Api.ViewModels.Arquivo;
using BikeRoubada.Api.ViewModels.Bicicleta;
using BikeRoubada.Api.ViewModels.Endereco;
using BikeRoubada.Api.ViewModels.Roubo;
using BikeRoubada.Api.ViewModels.TipoAlerta;
using BikeRoubada.Api.ViewModels.Usuario;
using BikeRoubada.Business.Models;
using NetTopologySuite.Geometries;
namespace BikeRoubada.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig() 
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();

            CreateMap<UsuarioApenasViewModel, Usuario>().ReverseMap();

            CreateMap<RegisterUserViewModel, Usuario>().ReverseMap();

            CreateMap<Alerta, AlertaApenasViewModel>().ReverseMap();
            CreateMap<Alerta, AlertaViewModel>().ReverseMap();


            //TipoAlerta
            CreateMap<TipoAlerta, TipoAlertaApenasViewModel>().ReverseMap();

            CreateMap<Point, SimplePoint>()
                .ForMember(src => src.X, map => map.MapFrom(src => src.X))
                .ForMember(src => src.Y, map => map.MapFrom(src => src.Y));

            CreateMap<SimplePoint, Point>()
                .ForMember(src => src.X, map => map.MapFrom(src => src.X))
                .ForMember(src => src.Y, map => map.MapFrom(src => src.Y));


            CreateMap<Bicicleta, BicicletaApenasViewModel>().ReverseMap();
            CreateMap<Bicicleta, BicicletaViewModel>().ReverseMap();

            CreateMap<Endereco, EnderecoApenasViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();

            CreateMap<Roubo, RouboApenasViewModel>().ReverseMap();
            CreateMap<Roubo, RouboViewModel>().ReverseMap();

            CreateMap<Arquivo, ArquivoApenasViewModel>().ReverseMap();
            CreateMap<Arquivo, ArquivoViewModel>().ReverseMap();
        }
    }
}
