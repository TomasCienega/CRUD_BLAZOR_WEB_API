using BlazorCrud.Server.Models;
using BlazorCrud.Server.Services.Contratos;
using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Services.Implementaciones
{
    public class EmpleadoServiceImpl : IEmpleadoService
    {
        private readonly DbCrudBlazorContext _dbContext;

        public EmpleadoServiceImpl(DbCrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseAPI<List<EmpleadoDTO>>> ListaEm()
        {
            var responseApi = new ResponseAPI<List<EmpleadoDTO>>();
            var listaEmpleadoDTO = new List<EmpleadoDTO>();

            try
            {
                // Hacemos el fetch de la base de datos
                var dbList = await _dbContext.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync();

                foreach (var item in dbList)
                {
                    listaEmpleadoDTO.Add(new EmpleadoDTO
                    {
                        IdEmpleado = item.IdEmpleado,
                        NombreCompleto = item.NombreCompleto,
                        IdDepartamento = item.IdDepartamento,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        Departamento = new DepartamentoDTO
                        {
                            IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = item.IdDepartamentoNavigation.Nombre
                        }
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaEmpleadoDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return responseApi;
        }

        public async Task<ResponseAPI<EmpleadoDTO>> BuscarEm(int id)
        {
            var responseApi = new ResponseAPI<EmpleadoDTO>();
            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    var empleadoDTO = new EmpleadoDTO
                    {
                        IdEmpleado = dbEmpleado.IdEmpleado,
                        NombreCompleto = dbEmpleado.NombreCompleto,
                        IdDepartamento = dbEmpleado.IdDepartamento,
                        Sueldo = dbEmpleado.Sueldo,
                        FechaContrato = dbEmpleado.FechaContrato
                    };

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = empleadoDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return responseApi;
        }

        public async Task<ResponseAPI<int>> GuardarEm(EmpleadoDTO empleado)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                // Mapeo de DTO a Entidad de Base de Datos
                var dbEmpleado = new Empleado
                {
                    NombreCompleto = empleado.NombreCompleto,
                    IdDepartamento = empleado.IdDepartamento,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato,
                };

                _dbContext.Empleados.Add(dbEmpleado);
                await _dbContext.SaveChangesAsync();

                if (dbEmpleado.IdEmpleado != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEmpleado.IdEmpleado;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no creado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return responseApi;
        }

        public async Task<ResponseAPI<int>> EditarEm(EmpleadoDTO empleado, int id)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                // 1. Buscamos el registro original en la DB
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    // 2. Sincronizamos los cambios (del DTO al modelo de DB)
                    dbEmpleado.NombreCompleto = empleado.NombreCompleto;
                    dbEmpleado.IdDepartamento = empleado.IdDepartamento;
                    dbEmpleado.Sueldo = empleado.Sueldo;
                    dbEmpleado.FechaContrato = empleado.FechaContrato;

                    _dbContext.Empleados.Update(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEmpleado.IdEmpleado;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Usuario no se pudo editar (no encontrado)";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return responseApi;
        }

        public async Task<ResponseAPI<bool>> EliminarEm(int id)
        {
            var responseApi = new ResponseAPI<bool>();
            try
            {
                // 1. Buscamos al empleado
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    // 2. Lo removemos del contexto y guardamos cambios
                    _dbContext.Empleados.Remove(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no encontrado para eliminar";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return responseApi;
        }
    }
}
