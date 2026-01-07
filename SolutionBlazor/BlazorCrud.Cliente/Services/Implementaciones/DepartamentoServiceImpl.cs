using BlazorCrud.Cliente.Services.Contratos;
using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;
using System.Net.Http.Json;

namespace BlazorCrud.Cliente.Services.Implementaciones
{
    public class DepartamentoServiceImpl : IDepartamentoService
    {
        private readonly HttpClient _httpClient;

        public DepartamentoServiceImpl(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DepartamentoDTO>> listarDeps()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<DepartamentoDTO>>>("api/Departamento/ListarDepartamentos");
            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }
    }
}
