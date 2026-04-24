using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManager.API.Shared.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Loga o erro detalhado no servidor (Serilog)
            _logger.LogError(exception, "Ocorreu um erro não tratado: {Message}", exception.Message);

            var code = HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                status = (int)code,
                message = "Ocorreu um erro interno no servidor. Tente novamente mais tarde.",
                detail = exception.Message // Detalhes do erro para desenvolvimento (remover ou ocultar em produção)
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}