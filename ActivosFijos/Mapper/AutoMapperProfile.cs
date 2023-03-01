using ActivosFijos.Model.DTO;
using ActivosFijos.Model;
using AutoMapper;

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

            //PUT Or UPDATE
            CreateMap<DepartamentoUpdateDTO, Departamento>();
            CreateMap<EmpleadoUpdateDTO, Empleado>();
            CreateMap<TipoActivoUpdateDTO, TipoActivo>();
            CreateMap<ActivoFijoUpdateDTO, ActivoFijo>();
            CreateMap<CalculoDepreciacionUpdateDTO, CalculoDepreciacion>();
            CreateMap<AsientosContablesUpdateDTO, AsientosContables>();
        }
    }
}
