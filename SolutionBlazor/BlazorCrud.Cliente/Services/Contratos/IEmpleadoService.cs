using BlazorCrud.Shared.DTOS;

namespace BlazorCrud.Cliente.Services.Contratos
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTO>> ListarEmpls();
        Task<EmpleadoDTO> buscarEmpl(int id);
        Task<int> guardarEmpl(EmpleadoDTO empleado);
        Task<int> editarEmpl(EmpleadoDTO empleado);
        Task<bool> eliminarEmpl(int id);
    }
}
