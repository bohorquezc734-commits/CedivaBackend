using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
// Descomenta la siguiente línea y ajusta el namespace donde tengas tus excepciones de dominio
// using Cediva.Domain.Exceptions; 

namespace Cediva.API.Middleware
{
    // Heredamos de IExceptionHandler, la interfaz moderna de .NET para manejar errores globales
    public class MiddlewareManejoExcepciones : IExceptionHandler
    {
        private readonly ILogger<MiddlewareManejoExcepciones> _logger;

        public MiddlewareManejoExcepciones(ILogger<MiddlewareManejoExcepciones> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // 1. Registramos el error en los logs del sistema
            _logger.LogError(exception, "Excepción capturada por el Middleware: {Message}", exception.Message);

            // 2. Preparamos la respuesta estándar (RFC 7807 Problem Details)
            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            // 3. Evaluamos de qué tipo es el error
            // NOTA: Cambia "ExcepcionDominio" por el nombre exacto de la clase de error que tengas en Cediva.Domain
            if (exception.GetType().Name == "ExcepcionDominio")
            {
                // Es un error de regla de negocio (Ej: Stock insuficiente)
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Error de validación de negocio";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Detail = exception.Message;
            }
            else
            {
                // Es un error del servidor no contemplado (Ej: Base de datos caída)
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Error interno del servidor";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = "Ha ocurrido un error inesperado. Por favor, contacte a soporte técnico.";
            }

            // 4. Escribimos la respuesta en formato JSON
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; // Indicamos que la excepción ya fue manejada y no debe propagarse más
        }
    }
}