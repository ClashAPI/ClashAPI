using System;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Helpers
{
    public class LogUserActivity
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContent = await next();
            var userId = int.Parse(resultContent.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContent.HttpContext.RequestServices.GetService<IRepository>();
            var user = await repo.GetUser(userId);
            user.LastActive = DateTime.Now;

            await repo.SaveAll();
        }
    }
}