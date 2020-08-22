using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class CustomAuthorizationRequirement:IAuthorizationRequirement
    {
        public IEnumerable<string> AllowScopes { get; set; }
        public CustomAuthorizationRequirement(IEnumerable<string> allowedScopes)
        {
            this.AllowScopes = allowedScopes;
        }
    }
}
