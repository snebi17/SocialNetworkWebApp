using web.Services;
using web.Helpers;
namespace web.Middlewares 
{
    public class JwtMiddleware
    {
            private readonly RequestDelegate next;

            public JwtMiddleware(RequestDelegate next) 
            {
                this.next = next;
            }

            public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = jwtUtils.ValidateToken(token);
                if (userId != null) 
                {
                    context.Items["User"] = userService.GetUser(userId.Value);
                }

                await next(context);
            }
    }
}