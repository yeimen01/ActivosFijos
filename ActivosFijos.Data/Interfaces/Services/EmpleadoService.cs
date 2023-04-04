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
    public class EmpleadoService<T> : IEmpleadoService<T>
    {
        public ApplicationDbContext DbContext { get; }
        public IMapper mapper { get; }

        public EmpleadoService(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        public async Task<Respuesta> Get(int id)
        {
            Respuesta respuesta;

            var empleado = mapper.Map<EmpleadoGetDTO>
                            (await DbContext.Empleado.
                            Include(x => x.Departamento).
                            FirstOrDefaultAsync(x => x.Id == id));

            if (empleado == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", empleado);
            }
            

            return respuesta;
        }

        public async Task<Respuesta> Get()
        {
            Respuesta respuesta;

            var empleados = mapper.Map<List<EmpleadoGetDTO>>(
                await DbContext.Empleado.
                Include(x=> x.Departamento).
                ToListAsync());

            if (empleados == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
            }
            else if (empleados.Count == 0)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NoContent, "Aún no se ha agregado ningún empleado");
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", empleados);
            }

            return respuesta;
        }

        public async Task<Respuesta> Post(EmpleadoCreateDTO empleadoCreateDTO)
        {
            Respuesta respuesta;

            //Verifying the exitanse of the department
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x=> x.Id == empleadoCreateDTO.DepartamentoId);

            if (departamento == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el departamento del empleado que desea agregar.");
            }
            else if (ValidacionesEmpleadoCreate(empleadoCreateDTO) != "")
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, ValidacionesEmpleadoCreate(empleadoCreateDTO));
            }
            else
            {
                //Mapping information
                Empleado empleado = mapper.Map<Empleado>(empleadoCreateDTO);

                //Adding the information
                DbContext.Add(empleado);
                await DbContext.SaveChangesAsync();

                //Mapping information to show
                EmpleadoGetDTO empleadoGetDTO = mapper.Map<EmpleadoGetDTO>(empleado);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Empleado creado correctamente.", empleadoGetDTO);
            }
            

            return respuesta;
        }

        public async Task<Respuesta> Put(EmpleadoUpdateDTO empleadoDTO, int id)
        {
            Respuesta respuesta;

            //Verifying existense
            var empleado = await DbContext.Empleado.Include(x=> x.Departamento).FirstOrDefaultAsync(x=> x.Id == id);
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x=> x.Id == empleadoDTO.DepartamentoId);

            //Verifying id
            if (empleadoDTO.Id != id)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "El id proporcionado no coincide con el id del empleado.");
            }
            else if (empleado == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el id del empleado que desea actualizar.");
            }
            else if (departamento == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el id del departamento que desea actualizar.");
            }
            else if(ValidacionesEmpleadoUpdate(empleadoDTO) != "")
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, ValidacionesEmpleadoUpdate(empleadoDTO));
            }
            else
            {
                //Mapping information
                mapper.Map(empleadoDTO, empleado);

                //Updating information
                DbContext.Update(empleado);
                await DbContext.SaveChangesAsync();

                //Mapping information
                EmpleadoGetDTO empleadoGetDTO = mapper.Map<EmpleadoGetDTO>(empleado);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Empleado actualizado correctamente", empleadoGetDTO);
            }

            return respuesta;
        }

        public async Task<Respuesta> Delete(int id)
        {
            Respuesta respuesta;

            //Verifying existense
            var existeEmpleado = await DbContext.Empleado.AnyAsync(x=> x.Id == id);

            if (!existeEmpleado)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el id del empleado que desea borrar.");
            }
            else
            {
                //Deleting information
                DbContext.Remove(new Empleado() { Id = id });
                await DbContext.SaveChangesAsync();

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Departamento borrado correctamente");
            }

            return respuesta;
        }

        private string ValidacionesEmpleadoCreate(EmpleadoCreateDTO empleadoCreateDTO)
        {
            string valido = "";

            if (!CedulaValida(empleadoCreateDTO.Cedula))
            {
                valido = "La cedula no es valida.\n";
            }

            if (empleadoCreateDTO.FechaIngreso > DateTime.Now)
            {
                valido = "La fecha de ingreso no puede ser a futuro.\n";
            }

            //Verifying tipo persona
            if (!Enum.IsDefined(typeof(TipoPersona), empleadoCreateDTO.TipoPersona))
            {
                valido = "El tipo de persona suministrado no existe.";
            }
            
            return valido;
        }

        private string ValidacionesEmpleadoUpdate(EmpleadoUpdateDTO empleadoUpdateDTO)
        {
            string valido = "";

            if (!CedulaValida(empleadoUpdateDTO.Cedula))
            {
                valido = "La cedula no es valida.\n";
            }

            if (empleadoUpdateDTO.FechaIngreso > DateTime.Now)
            {
                valido = "La fecha de ingreso no puede ser a futuro.\n";
            }

            if (!Enum.IsDefined(typeof(TipoPersona), empleadoUpdateDTO.TipoPersona))
            {
                valido = "El tipo de persona suministrado no existe.\n";
            }

            //if (!Enum.IsDefined(typeof(Estado), empleadoUpdateDTO.Estado))
            //{
            //    valido = "El estado suminstrado no existe.";
            //}

            return valido;
        }

        public bool CedulaValida(string Cedula)
        {
            {
                int Total = 0;
                string vcCedula = Cedula.Replace("-", "");
                int LongCed = vcCedula.Trim().Length;
                int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };
                if (LongCed < 11 || LongCed > 11)
                    return false;
                for (int vDig = 1; vDig <= LongCed; vDig++)
                {
                    int Calculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                    if (Calculo < 10)
                        Total += Calculo;
                    else
                        Total += Int32.Parse(Calculo.ToString().Substring(0, 1)) + Int32.Parse(Calculo.ToString().Substring(1, 1));
                }
                if (Total % 10 == 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
