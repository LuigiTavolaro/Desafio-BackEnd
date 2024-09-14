using System.Diagnostics;



namespace DesafioBackEndProject.Middlewares;
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        // Cria um escopo de log para a requisição atual
        using (_logger.BeginScope(new { RequestId = Guid.NewGuid(), Method = context.Request.Method, Path = context.Request.Path }))
        {
            _logger.LogInformation("Handling request");

            try
            {
                // Chama o próximo middleware na cadeia
                await _next(context);
            }
            finally
            {
                watch.Stop();
                _logger.LogInformation("Finished handling request. Status Code: {StatusCode}, Duration: {ElapsedMilliseconds}ms",
                    context.Response.StatusCode, watch.Elapsed.TotalMilliseconds);
            }
        }
    }
}
