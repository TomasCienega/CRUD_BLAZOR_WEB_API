using BlazorCrud.Server.Models;
using BlazorCrud.Server.Services.Contratos;
using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Services.Implementaciones
{
    public class DepartamentoServiceImpl : IDepartamentoService
    {
        private readonly DbCrudBlazorContext _dbContext;

        public DepartamentoServiceImpl(DbCrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ResponseAPI<List<DepartamentoDTO>>> ListaDep()
        {
            var responseApi = new ResponseAPI<List<DepartamentoDTO>>();
            try
            {
                var listaDTO = new List<DepartamentoDTO>();
                var dbList = await _dbContext.Departamentos.ToListAsync();

                foreach (var item in dbList)
                {
                    listaDTO.Add(new DepartamentoDTO
                    {
                        IdDepartamento = item.IdDepartamento,
                        Nombre = item.Nombre
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaDTO;
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
