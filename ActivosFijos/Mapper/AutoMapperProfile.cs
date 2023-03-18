using ActivosFijos.Model.DTO;
using AutoMapper;
using ActivosFijos.Model.Entities;

namespace ActivosFijos.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //POST Or ADD
            CreateMap<DepartamentoCreateDTO, Departamento>();
            CreateMap<EmpleadoCreateDTO, Empleado>();
            CreateMap<TipoActivoCreateDTO, TipoActivo>();
            CreateMap<ActivoFijoCreateDTO, ActivoFijo>();
            CreateMap<CalculoDepreciacionCreateDTO, CalculoDepreciacion>();
            CreateMap<AsientosContablesCreateDTO, AsientosContables>();

            //GET
            CreateMap<Empleado, EmpleadoGetDTO>()
                      .ForMember(dest => dest.DepartamentoDescripcion,opt => opt.MapFrom(src => src.Departamento.Descripcion));

            CreateMap<Departamento, DepartamentoGetDTO>().ReverseMap();
            CreateMap<TipoActivo, TipoActivoGetDTO>().ReverseMap();

            CreateMap<ActivoFijo, ActivoFijoGetDTO>().
                ForMember(dest => dest.DescripcionDepartamento,opt => opt.MapFrom(src=> src.Departamento.Descripcion)).
                ForMember(dest=> dest.DescripcionTipoActivo, opt => opt.MapFrom(src=> src.TipoActivo.Descripcion)).
                ForMember(dest=> dest.CuentaContableCompra, opt => opt.MapFrom(src=> src.TipoActivo.CuentaContableCompra)).
                ForMember(dest=> dest.CuentaContableDepreciacion, opt => opt.MapFrom(src=> src.TipoActivo.CuentaContableDepreciacion));

            CreateMap<CalculoDepreciacion, CalculoDepreciacionGetDTO>().
                ForMember(dest => dest.DescripcionActivosFijos, opt => opt.MapFrom(src => src.ActivosFijos.Descripcion));

            //PUT Or UPDATE
            CreateMap<DepartamentoUpdateDTO, Departamento>();
            CreateMap<EmpleadoUpdateDTO, Empleado>();
            CreateMap<TipoActivoUpdateDTO, TipoActivo>();
            CreateMap<ActivoFijoUpdateDTO, ActivoFijo>();
            CreateMap<ActivoFijoUpdateDTO, ActivoFijoGetDTO>().ReverseMap();
            CreateMap<CalculoDepreciacionUpdateDTO, CalculoDepreciacion>().ReverseMap();
            CreateMap<AsientosContablesUpdateDTO, AsientosContables>();
        }
    }
}
