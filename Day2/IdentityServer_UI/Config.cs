// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Collections.Generic;

namespace IdentityServer_UI
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("api","my api"){ Scopes={ "api" } }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api","my api")
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client{ 
                    ClientId="mvc",
                    ClientSecrets={ new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.Code,
                    RedirectUris={ "http://localhost:5001/signin-oidc" },
                    PostLogoutRedirectUris={ "http://localhost:5001/signout-callback-oidc"},
                    AllowedScopes=new List<string>{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "api"
                    },
                    //RequirePkce=false,
                    //AllowAccessTokensViaBrowser=true,
                    //AlwaysIncludeUserClaimsInIdToken=true
                }
            };
    }
}