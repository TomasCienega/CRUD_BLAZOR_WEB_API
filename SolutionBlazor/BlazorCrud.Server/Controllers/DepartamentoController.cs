using BlazorCrud.Server.Models;
using BlazorCrud.Server.Services.Contratos;
using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        //Forma tutorial
        #region
        //private readonly DbCrudBlazorContext _dbContext;

        //public DepartamentoController(DbCrudBlazorContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        #endregion

        //Forma tutorial
        #region
        //[HttpGet]
        //[Route("ListarDepartamentos")]
        //public async Task<IActionResult> ListarDepartamentos()
        //{
        //    var responseApi = new ResponseAPI<List<DepartamentoDTO>>();
        //    var listaDepartamentoDTO = new List<DepartamentoDTO>();

        //    try
        //    {

        //        foreach (var item in await _dbContext.Departamentos.ToListAsync())
        //        {
        //            listaDepartamentoDTO.Add(new DepartamentoDTO
        //            {
        //                IdDepartamento = item.IdDepartamento,
        //                Nombre = item.Nombre
        //            });
        //        }
        //        responseApi.EsCorrecto = true;
        //        responseApi.Valor = listaDepartamentoDTO;

        //    }
        //    catch (Exception ex)
        //    {
        //        responseApi.EsCorrecto = false;
        //        responseApi.Mensaje = ex.Message;
        //    }
        //    return Ok(responseApi);
        //}
        #endregion

        [HttpGet]
        [Route("ListarDepartamentos")]
        public async Task<IActionResult> ListarDepartamentos()
        {
            var response = await _departamentoService.ListaDep();
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
    }
}
