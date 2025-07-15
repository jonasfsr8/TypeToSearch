using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;
using TypeToSearch.Domain.Dtos.Responses;
using TypeToSearch.Domain.Exceptions;

namespace TypeToSearch.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    await HandleOverviewExceptionAsync(context, context.Response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ocorreu um erro inesperado.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = CreateErrorResponse(exception);
            return WriteResponseAsync(context, response);
        }

        private Task HandleOverviewExceptionAsync(HttpContext context, int statusCode)
        {
            var response = CreateOverviewErrorResponse(statusCode);
            return WriteResponseAsync(context, response);
        }

        private GenericResponse<object> CreateErrorResponse(Exception exception)
        {
            return exception switch
            {
                UnauthorizedAccessException _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.Unauthorized
                },
                InactiveException _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.Forbidden
                },
                SqlException _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                },
                TimeoutException _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.RequestTimeout
                },
                NotFoundException _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.NotFound
                },
                ResourceAlreadyExistsException _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest
                },
                ArgumentNullException _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest
                },
                ArgumentException _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest
                },
                _ => new GenericResponse<object>
                {
                    Content = null,
                    Msg = exception.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest
                }
            };
        }

        private GenericResponse<object> CreateOverviewErrorResponse(int statusCode)
        {
            return new GenericResponse<object>
            {
                Content = null,
                Msg = statusCode switch
                {
                    (int)HttpStatusCode.Unauthorized => "acesso não autorizado. Token inválido ou ausente.",
                    (int)HttpStatusCode.Forbidden => "o servidor entendeu a solicitação, mas se recusa a atendê-la",
                    _ => "Erro inesperado."
                },
                StatusCode = statusCode
            };
        }

        private Task WriteResponseAsync(HttpContext context, GenericResponse<object> response)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = response.StatusCode;
            }

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
