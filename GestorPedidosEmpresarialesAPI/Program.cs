using GestorPedidosEmpresarialesBackend.Business;
using GestorPedidosEmpresarialesBackend.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<UsuarioData>();       // Inyección de UsuarioData
builder.Services.AddScoped<UsuarioBusiness>();   // Inyección de UsuarioBusiness

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
