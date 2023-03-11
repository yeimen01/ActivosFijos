using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Enum;
using ActivosFijos.Model.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces.Services
{
    public class DepartamentoService<T> : IDepartamentoService<T>
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public DepartamentoService(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        public async Task<DepartamentoGetDTO> Get(int id)
        {
            var departamento = await DbContext.Departamento.
                Select(x => new DepartamentoGetDTO
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Estado = Utilities.Estado(x.Estado),
                    Empleados = x.Empleados
                }).Where(x=> x.Id == id).
                FirstOrDefaultAsync();

            return departamento;
        }

        public async Task<List<DepartamentoGetDTO>> Get()
        {
            var Departamentos = await DbContext.Departamento.
                Select( x=> new DepartamentoGetDTO { 
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Estado = Utilities.Estado(x.Estado),
                    Empleados = x.Empleados
                }).ToListAsync();

            return Departamentos;
        }

        public async Task<Departamento> Post(DepartamentoCreateDTO departamentoDTO)
        {
            //Mapping information
            Departamento departamento = mapper.Map<Departamento>(departamentoDTO);

            //Adding the information
            DbContext.Add(departamento);
            await DbContext.SaveChangesAsync();

            return departamento;
        }

        public async Task Put(DepartamentoUpdateDTO departamentoDTO, DepartamentoGetDTO departamento)
        {
            //Mapping information
            mapper.Map(departamentoDTO, departamento);

            //Updating information
            DbContext.Entry(departamento).State = EntityState.Modified;
            DbContext.Update(departamento);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            //Deleting information
            DbContext.Remove(new Departamento() { Id = id });
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(DepartamentoGetDTO departamento)
        {
            //Deleting information
            DbContext.Remove(departamento);
            await DbContext.SaveChangesAsync();
        }
    }
}
