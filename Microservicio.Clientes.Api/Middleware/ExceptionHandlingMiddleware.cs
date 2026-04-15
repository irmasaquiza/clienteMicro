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
            catch (Exception)
            {
                await HandleGenericException(context);
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
        private static async Task HandleGenericException(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiErrorResponse
            {
                Message = "Ocurrió un error interno en el servidor"
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}