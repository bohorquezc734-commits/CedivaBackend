using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cediva.Dominio.Excepciones;
using Cediva.Domain.Excepciones;

namespace Cediva.API.Middleware
{
    public class MiddlewareManejoExcepciones : IExceptionHandler
    {
        private readonly ILogger<MiddlewareManejoExcepciones> _logger;
        private readonly IHostEnvironment _env;

        public MiddlewareManejoExcepciones(ILogger<MiddlewareManejoExcepciones> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
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
            if (exception is ReglaDeNegocioException reglaNegocioEx)
            {
                // Es un error de regla de negocio específico
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Error de Regla de Negocio";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Detail = reglaNegocioEx.Message;
            }
            else if (exception is ExcepcionDominio excepcionDominio)
            {
                // Es un error de regla de negocio de dominio existente
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Error de validación de negocio";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Detail = excepcionDominio.Message;
            }
            else
            {
                // Es un error inesperado (Ej: Fallo en BD, falta de configuración, etc.)
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Error interno del servidor";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = _env.IsDevelopment()
                    ? $"{exception.Message} ({exception.GetType().Name})"
                    : "Ha ocurrido un error inesperado. Por favor, contacte a soporte técnico.";
            }

            // 4. Escribimos la respuesta en formato JSON
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
