namespace LR5.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync(ex);
                throw;
            }
        }

        private Task LogExceptionAsync(Exception ex)
        {
            var logPath = "logs/exceptions.txt";
            var message = $"{DateTime.Now}: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
            return File.AppendAllTextAsync(logPath, message);
        }
    }
}
