using Microsoft.Extensions.DependencyInjection;
using RulesEnginePOC.Service;

namespace RulesEnginePOC
{
    public class UserRulesMiddleware
    {
        private readonly RequestDelegate _next;

        public UserRulesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                if (context.Request.Headers.TryGetValue("X-User-Id", out var userIdHeader))
                {
                    if (int.TryParse(userIdHeader, out var userId))
                    {
                        var rules = await userService.GetUserRoles(userId);

                        if (rules != null && rules.Any())
                        {
                            context.Items["EntitlementRules"] = rules;
                        }
                    }
                }
            }

            await _next(context);
        }
    }


}
