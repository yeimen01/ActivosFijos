using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Enum;
using ActivosFijos.Model.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<Respuesta> Get(int id)
        {
            Respuesta respuesta;

            var departamento = mapper.Map<DepartamentoGetDTO>(await DbContext.Departamento.
                Include(x => x.Empleados).
                FirstOrDefaultAsync(x=> x.Id == id));

            if (departamento == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", departamento);
            }

            return respuesta;
        }

        public async Task<Respuesta> Get()
        {
            Respuesta respuesta;

            var Departamentos = mapper.Map<List<DepartamentoGetDTO>>(await DbContext.Departamento.Include(x=> x.Empleados).ToListAsync());

            if (Departamentos == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            } 
            else if(Departamentos.Count == 0)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NoContent, "Aún no se ha agregado ningún departamento");
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", Departamentos);
            }

            return respuesta;
        }

        public async Task<Respuesta> Post(DepartamentoCreateDTO departamentoDTO)
        {
            //Mapping information
            Departamento departamento = mapper.Map<Departamento>(departamentoDTO);

            //Adding the information
            DbContext.Add(departamento);
            await DbContext.SaveChangesAsync();

            //Mapping information to show
            DepartamentoGetDTO departamentoGetDTO = mapper.Map<DepartamentoGetDTO>(departamento);

            //Respuesta
            Respuesta respuesta = Utilities.Respuesta(HttpStatusCode.Created, "Departamento creado correctamente", departamentoGetDTO);

            return respuesta;
        }

        public async Task<Respuesta> Put(DepartamentoUpdateDTO departamentoDTO, int id)
        {
            Respuesta respuesta;

            //Verifying existense
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x => x.Id == id);

            //Verifying id
            if (departamentoDTO.Id != id)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "El id proporcionado no coincide con el id del departamento.");
            }
            else if (departamento == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encontró el departamento.");
            }
            //Verifying if enum is correct
            else if (!Enum.IsDefined(typeof(Estado), departamento.Estado))
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El estado suminstrado no existe.");
            }
            else
            {
                //Mapping information
                mapper.Map(departamentoDTO, departamento);

                //Updating information
                DbContext.Update(departamento);
                await DbContext.SaveChangesAsync();

                //Mapping information to show
                DepartamentoGetDTO departamentoGetDTO = mapper.Map<DepartamentoGetDTO>(departamento);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Departamento actualizado correctamente", departamentoGetDTO);
            }

            return respuesta;
        }

        public async Task<Respuesta> Delete(int id)
        {
            Respuesta respuesta;

            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x=>x.Id == id);

            if (departamento == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encontró el departamento.");
            }

            else if (departamento.Empleados != null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "No se puede borrar el departamento, porque ya tiene empleados.");
            }
            else
            {
                //Deleting information
                DbContext.Remove(departamento);
                await DbContext.SaveChangesAsync();

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Departamento borrado correctamente");
            }

            return respuesta;
        }

    }
}
