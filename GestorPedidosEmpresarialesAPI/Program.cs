using GestorPedidosEmpresarialesBackend.Business;
using GestorPedidosEmpresarialesBackend.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<UsuarioData>();
builder.Services.AddScoped<UsuarioBusiness>();
builder.Services.AddScoped <ClienteData>();
builder.Services.AddScoped<ClienteBusiness>();
builder.Services.AddScoped<ParametrosSistemaData>();
builder.Services.AddScoped<ParametrosSistemaBusiness>();
builder.Services.AddScoped<ProductoBaseData>();
builder.Services.AddScoped<ProductoBaseBusiness>();
builder.Services.AddScoped<ProductoVarianteData>();
builder.Services.AddScoped<ProductoVarianteBusiness>();
builder.Services.AddScoped<MetodoPagoData>();
builder.Services.AddScoped<MetodoPagoBusiness>();
builder.Services.AddScoped<PagoData>();
builder.Services.AddScoped<PagoBusiness>();
builder.Services.AddScoped<OrdenData>();
builder.Services.AddScoped<OrdenBusiness>();


// ¡AÑADE ESTAS LÍNEAS!
// Registro para Rol
builder.Services.AddScoped<RolData>();
builder.Services.AddScoped<RolBusiness>();

// Registro para Departamento
builder.Services.AddScoped<DepartamentoData>();
builder.Services.AddScoped<DepartamentoBusiness>();

// Registro para Empleado
builder.Services.AddScoped<EmpleadoData>();
builder.Services.AddScoped<EmpleadoBusiness>();

//Registro para Categoria
builder.Services.AddScoped<CategoriaData>();
builder.Services.AddScoped<CategoriaBusiness>();

builder.Services.AddControllers();

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