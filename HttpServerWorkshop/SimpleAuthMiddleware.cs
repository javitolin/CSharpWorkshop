namespace HttpServerWorkshop
{
    public class SimpleAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SimpleAuthMiddleware> _logger;

        private string getUser(string jwt) => jwt;

        public SimpleAuthMiddleware(RequestDelegate next, ILogger<SimpleAuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Using authentication");

            string? jwt = context.Request.Headers["x-access-token"];
            context.Request.Headers["user"] = getUser(jwt ?? "UNAUTH");

            await _next(context);
        }
    }

}
