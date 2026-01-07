using Azure;
using BlazorCrud.Server.Models;
using BlazorCrud.Server.Services.Contratos;
using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }
        //FormaTutorial
        #region
        //private readonly DbCrudBlazorContext _dbContext;

        //public EmpleadoController(DbCrudBlazorContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        #endregion

        //FormaTutorial
        #region 
        //[HttpGet]
        //[Route("/ListaEmpleados")]
        //public async Task<IActionResult> ListaEmpleados()
        //{
        //    var responseApi = new ResponseAPI<List<EmpleadoDTO>>();
        //    var listaEmpleadoDTO = new List<EmpleadoDTO>();
        //    try
        //    {
        //        foreach (var item in await _dbContext.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
        //        {
        //            listaEmpleadoDTO.Add(new EmpleadoDTO
        //            {
        //                IdEmpleado = item.IdEmpleado,
        //                NombreCompleto = item.NombreCompleto,
        //                IdDepartamento = item.IdDepartamento,
        //                Sueldo = item.Sueldo,
        //                FechaContrato = item.FechaContrato,
        //                Departamento = new DepartamentoDTO
        //                {
        //                    IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
        //                    Nombre = item.IdDepartamentoNavigation.Nombre
        //                }
        //            });
        //        }
        //        responseApi.EsCorrecto = true;
        //        responseApi.Valor = listaEmpleadoDTO;
        //    }
        //    catch (Exception ex)
        //    {

        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }
        //    return Ok(responseApi);
        //}
        #endregion

        #region
        [HttpGet]
        [Route("ListaEmpleados")]
        public async Task<IActionResult> ListaEmpleados()
        {
            var response = await _empleadoService.ListaEm();
            if (response.EsCorrecto)
            {
                // Esto devuelve un Status 200 y el JSON de tu ResponseAPI
                return Ok(response);
            }
            else
            {
                // Esto devuelve un Status 500 o 400 y el mensaje de error que capturó el Service
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        #endregion

        //FormaTutorial
        #region
        //[HttpGet]
        //[Route("/BuscarEmpleado/{id}")]
        //public async Task<IActionResult> BuscarEmpleado(int id)
        //{
        //    var responseApi = new ResponseAPI<EmpleadoDTO>();
        //    var empleadoDTO = new EmpleadoDTO();
        //    try
        //    {
        //        var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
        //        if (dbEmpleado != null)
        //        {
        //            empleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
        //            empleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
        //            empleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
        //            empleadoDTO.Sueldo = dbEmpleado.Sueldo;
        //            empleadoDTO.FechaContrato = dbEmpleado.FechaContrato;

        //            responseApi.EsCorrecto = true;
        //            responseApi.Valor = empleadoDTO;
        //        }
        //        else
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "Usuario no encontrado";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }
        //    return Ok(responseApi);
        //}
        #endregion

        #region
        [HttpGet]
        [Route("BuscarEmpleado/{id}")]
        public async Task<IActionResult> BuscarEmpleado(int id)
        {
            var response = await _empleadoService.BuscarEm(id);
            if (response.EsCorrecto)
                return Ok(response);
            else
                return StatusCode(StatusCodes.Status404NotFound, response);
            // Nota: En búsquedas por ID, si no existe, el estándar es 404 (NotFound)
        }
        #endregion

        //FormaTutorial
        #region
        //[HttpPost]
        //[Route("/GuardarEmpleado")]
        //public async Task<IActionResult> GuardarEmpleado([FromBody] EmpleadoDTO empleado)
        //{

        //    var responseApi = new ResponseAPI<int>();
        //    try
        //    {
        //        var dbEmpleado = new Empleado
        //        {
        //            NombreCompleto = empleado.NombreCompleto,
        //            IdDepartamento = empleado.IdDepartamento,
        //            Sueldo = empleado.Sueldo,
        //            FechaContrato = empleado.FechaContrato,
        //        };

        //        _dbContext.Empleados.Add(dbEmpleado);
        //        await _dbContext.SaveChangesAsync();

        //        if (dbEmpleado.IdEmpleado != 0)
        //        {
        //            responseApi.EsCorrecto = true;
        //            responseApi.Valor = dbEmpleado.IdEmpleado;
        //        }
        //        else
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "Usuario no creado";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }
        //    return Ok(responseApi);

        //}
        #endregion

        #region
        [HttpPost]
        [Route("GuardarEmpleado")]
        public async Task<IActionResult> GuardarEmpleado([FromBody] EmpleadoDTO empleado)
        {
            var response = await _empleadoService.GuardarEm(empleado);

            if (response.EsCorrecto)
                return Ok(response);
            else
                return BadRequest(response); // Usamos BadRequest para indicar que hubo un problema al crear
        }
        #endregion

        //FormaTutorial
        #region
        //[HttpPut]
        //[Route("/EditarEmpleado/{id}")]
        //public async Task<IActionResult> EditarEmpleado([FromBody] EmpleadoDTO empleado, int id)
        //{
        //    var responseApi = new ResponseAPI<int>();
        //    try
        //    {
        //        var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

        //        if (dbEmpleado != null)
        //        {

        //            dbEmpleado.NombreCompleto = empleado.NombreCompleto;
        //            dbEmpleado.IdDepartamento = empleado.IdDepartamento;
        //            dbEmpleado.Sueldo = empleado.Sueldo;
        //            dbEmpleado.FechaContrato = empleado.FechaContrato;

        //            _dbContext.Empleados.Update(dbEmpleado);
        //            await _dbContext.SaveChangesAsync();

        //            responseApi.EsCorrecto = true;
        //            responseApi.Valor = dbEmpleado.IdEmpleado;
        //        }
        //        else
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "Usuario no se pudo editar";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }
        //    return Ok(responseApi);
        //}
        #endregion

        #region
        [HttpPut]
        [Route("EditarEmpleado/{id}")]
        public async Task<IActionResult> EditarEmpleado([FromBody] EmpleadoDTO empleado, int id)
        {
            var response = await _empleadoService.EditarEm(empleado, id);

            if (response.EsCorrecto)
                return Ok(response);
            else
                return BadRequest(response); // O NotFound según prefieras
        }
        #endregion

        //FormaTutorial
        #region
        //[HttpDelete]
        //[Route("/EliminarEmpleado/{id}")]
        //public async Task<IActionResult> EliminarEmpleado(int id)
        //{
        //    var responseApi = new ResponseAPI<int>();
        //    try
        //    {
        //        var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

        //        if (dbEmpleado != null)
        //        {


        //            _dbContext.Empleados.Remove(dbEmpleado);
        //            await _dbContext.SaveChangesAsync();

        //            responseApi.EsCorrecto = true;
        //        }
        //        else
        //        {
        //            responseApi.EsCorrecto = false;
        //            responseApi.Mensaje = "Usuario no se pudo eliminar";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }
        //    return Ok(responseApi);
        //}
        #endregion

        #region
        [HttpDelete]
        [Route("EliminarEmpleado/{id}")]
        public async Task<IActionResult> EliminarEmpleado( int id)
        {
            var response = await _empleadoService.EliminarEm(id);

            if (response.EsCorrecto)
                return Ok(response);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        #endregion
    }
}
