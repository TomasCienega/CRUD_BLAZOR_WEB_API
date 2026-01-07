using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;

namespace BlazorCrud.Server.Services.Contratos
{
    public interface IEmpleadoService
    {
        Task<ResponseAPI<List<EmpleadoDTO>>> ListaEm();
        Task<ResponseAPI<EmpleadoDTO>> BuscarEm(int id);
        Task<ResponseAPI<int>> GuardarEm(EmpleadoDTO empleado);
        Task<ResponseAPI<int>> EditarEm(EmpleadoDTO empleado, int id);
        Task<ResponseAPI<bool>> EliminarEm(int id);
    }
}
