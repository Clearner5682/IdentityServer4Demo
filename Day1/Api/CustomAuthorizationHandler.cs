using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<CustomAuthorizationHandler> logger;

        public CustomAuthorizationHandler(IHttpContextAccessor httpContextAccessor,ILogger<CustomAuthorizationHandler> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            this.logger.LogInformation("请求URL:"+this.httpContextAccessor.HttpContext.Request.Path.Value);
            var scopes = context.User.Claims.Where(o => o.Type.ToLower() == "scope").Select(o=>o.Value);
            var intersectList = requirement.AllowScopes.Intersect(scopes);
            if (intersectList.Any())
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            context.Fail();

            return Task.CompletedTask;
        }
    }
}
