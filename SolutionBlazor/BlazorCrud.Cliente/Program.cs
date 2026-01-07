 using BlazorCrud.Cliente;
using BlazorCrud.Cliente.Services.Contratos;
using BlazorCrud.Cliente.Services.Implementaciones;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5117") });
builder.Services.AddScoped<IDepartamentoService,DepartamentoServiceImpl>();
builder.Services.AddScoped<IEmpleadoService,EmpleadoServiceImpl>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
