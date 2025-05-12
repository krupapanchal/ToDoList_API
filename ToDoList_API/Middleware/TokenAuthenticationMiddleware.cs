namespace ToDoList_API.Middleware
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("ApiToken", out var extractedToken))
                throw new UnauthorizedAccessException("API Token is missing.");

            var token = _configuration.GetValue<string>("ApiToken");

            if (!token.Equals(extractedToken))
                throw new UnauthorizedAccessException("Invalid API Token.");

            await _next(context);
        }
    }
}
