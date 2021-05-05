using System;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace api.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
                return;

            var username = resultContext.HttpContext.User
                                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var repo = resultContext.HttpContext.RequestServices
                                    .GetService<IUserRepository>();
            var user = await repo.GetUserByUsernameAsync(username);
            user.LastActive = DateTime.Now;
            await repo.SaveAllAsync();
        }
    }
}
