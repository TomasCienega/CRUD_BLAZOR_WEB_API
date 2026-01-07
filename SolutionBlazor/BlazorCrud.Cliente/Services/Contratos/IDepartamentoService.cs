using BlazorCrud.Shared.DTOS;

namespace BlazorCrud.Cliente.Services.Contratos
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoDTO>> listarDeps();
    }
}
