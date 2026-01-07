using BlazorCrud.Cliente.Services.Contratos;
using BlazorCrud.Shared.DTOS;
using BlazorCrud.Shared.Respuesta;
using System.Net.Http.Json;

namespace BlazorCrud.Cliente.Services.Implementaciones
{
    public class EmpleadoServiceImpl : IEmpleadoService
    {
        private HttpClient _httpClient;

        public EmpleadoServiceImpl(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmpleadoDTO>> ListarEmpls()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<EmpleadoDTO>>>("api/Empleado/ListaEmpleados");
            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<EmpleadoDTO> buscarEmpl(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<EmpleadoDTO>>($"api/Empleado/BuscarEmpleado/{id}");
            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);
            }
        }

        public async Task<int> guardarEmpl(EmpleadoDTO empleado)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Empleado/GuardarEmpleado", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<int> editarEmpl(EmpleadoDTO empleado)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Empleado/EditarEmpleado/{empleado.IdEmpleado}", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }

        public async Task<bool> eliminarEmpl(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Empleado/EliminarEmpleado/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();
            if (response!.EsCorrecto)
            {
                return response.EsCorrecto!;
            }
            else
            {
                throw new Exception(response.Mensaje);
            }
        }




    }
}
