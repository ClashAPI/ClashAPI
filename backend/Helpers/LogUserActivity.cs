using System;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Data;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Helpers
{
    public class LogUserActivity
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContent = await next();
            var userId = Guid.Parse(resultContent.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContent.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await repo.GetByIdAsync(userId);
            user.LastActive = DateTime.Now;

            await repo.SaveAllAsync();
        }
    }
}