using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AyudaExamen.Policies
{
    public class CreatePedidoRequirement : AuthorizationHandler<CreatePedidoRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatePedidoRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            var httpContext = filterContext.HttpContext;

            //RepositoryComics repo = httpContext.RequestServices.GetService<RepositoryComics>();

            if (context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier) == false)
            {
                context.Fail();
            }
            else
            {
                string role = context.User.FindFirstValue(ClaimTypes.Role);
                if (role != "admin")
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
