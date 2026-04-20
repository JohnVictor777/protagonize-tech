using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Context;

namespace TaskManager.API.Shared.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                using (LogContext.PushProperty("RequestMethod", context.Request.Method))
                using (LogContext.PushProperty("RequestPath", context.Request.Path))
                {
                    await _next(context);
                }
            }
            finally
            {
                sw.Stop();
                var statusCode = context.Response?.StatusCode ?? 0;

                if (statusCode >= 500)
                {
                    _logger.LogError("Erro {StatusCode} em {RequestMethod} {RequestPath} ({ElapsedMs} ms)",
                        statusCode, context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);
                }
                else if (statusCode >= 400)
                {
                    _logger.LogWarning("Aviso {StatusCode} em {RequestMethod} {RequestPath} ({ElapsedMs} ms)",
                        statusCode, context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);
                }
                else
                {
                    _logger.LogInformation("OK {StatusCode} em {RequestMethod} {RequestPath} ({ElapsedMs} ms)",
                        statusCode, context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);
                }
            }
        }
    }
}