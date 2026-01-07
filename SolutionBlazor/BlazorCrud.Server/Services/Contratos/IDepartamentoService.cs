using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;

namespace BlazorCrud.Server.Services.Contratos
{
    public interface IDepartamentoService
    {
        Task<ResponseAPI<List<DepartamentoDTO>>> ListaDep();
    }
}
