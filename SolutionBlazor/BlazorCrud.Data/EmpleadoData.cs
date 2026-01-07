using BlazorCrud.Server.Models;
using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Data
{
    public class EmpleadoData
    {
        private readonly DbCrudBlazorContext _dbContext;

        public EmpleadoData(DbCrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseAPI<int>> GuardarEmpleadoData(EmpleadoDTO empleado)
        {
             
            var responseApi = new ResponseAPI<int>();
            try
            {
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
    }
}
