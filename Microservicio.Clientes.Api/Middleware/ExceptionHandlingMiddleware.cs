using System.Net;
using System.Text.Json;
using Microservicio.Clientes.Business.Exceptions;
using Microservicio.Clientes.Api.Models;

namespace Microservicio.Clientes.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                await HandleBusinessException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericException(context, ex);
            }
        }

        // 🔥 MANEJO DE EXCEPCIONES DE NEGOCIO
        private static async Task HandleBusinessException(HttpContext context, BusinessException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            var response = new ApiErrorResponse
            {
                Message = ex.Message
            };

            // 🔥 SI ES VALIDATION → LISTA DE ERRORES
            if (ex is ValidationException validationException)
            {
                response.Errors = validationException.Errors;
            }

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }

        // 🔥 MANEJO DE ERROR GENERAL
        private static async Task HandleGenericException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new ApiErrorResponse
            {
                Message = ex.Message // 🔥 VER ERROR REAL
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}