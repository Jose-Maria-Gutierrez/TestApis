using Microsoft.Extensions.Options;
using TestApis.Helpers;
using TestApis.Service;

namespace TestApis.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            this._next = next;
            this._appSettings = appSettings.Value;
        }
        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(); //token
            int? userId = jwtUtils.ValidateJwtToken(token); //valida y devuelve el id
            if (userId != null)
            {
                context.Items["User"] = userService.GetById(userId.Value); //devuelve el user con ese id en el HttpContext
            }
            await _next(context); //llama el prox middleware
        }

    }
}
