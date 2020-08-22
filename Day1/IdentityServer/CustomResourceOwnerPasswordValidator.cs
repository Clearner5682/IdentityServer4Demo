using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ILogger<CustomResourceOwnerPasswordValidator> logger;

        public CustomResourceOwnerPasswordValidator(ILogger<CustomResourceOwnerPasswordValidator> logger)
        {
            this.logger = logger;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            this.logger.LogInformation($"Username:{context.UserName},Password:{context.Password}");
            var user = Config.TestUsers.FirstOrDefault(o => o.Username == context.UserName);
            var identity = new ClaimsIdentity(user.Claims);
            var principal = new ClaimsPrincipal(identity);
            context.Result = new GrantValidationResult(subject: user.SubjectId, authenticationMethod: "password", user.Claims);

            return Task.CompletedTask;
        }
    }
}
