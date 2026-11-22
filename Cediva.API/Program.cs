using Cediva.Application;
using Cediva.Infrastructure;
using Cediva.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Swagger (Generador de documentación)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Inyectar dependencias de tu arquitectura (Aplicación y Persistencia)
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 3. Configurar manejo de excepciones
builder.Services.AddExceptionHandler<MiddlewareManejoExcepciones>();
builder.Services.AddProblemDetails();

// 4. Agregar controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // 5. Activar Swagger visual
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
