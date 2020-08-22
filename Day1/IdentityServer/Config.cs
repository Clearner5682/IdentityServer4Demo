// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser{ SubjectId=Guid.NewGuid().ToString(), Username="hongyan",Password="123456",Claims=new List<Claim>{ new Claim("Gender","男"),new Claim("Age","29")} }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("api","my api")
                {
                    Scopes=new string[]{ "api.weatherforecast","api.test","api.identity" }
                },
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api.weatherforecast"),
                new ApiScope("api.test"),
                new ApiScope("api.identity")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientId="client1",
                    ClientName="client1",
                    ClientSecrets=new List<Secret>{ new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes=new List<string>{ "api.weatherforecast", "api.test","api.identity" }
                },
                new Client()
                {
                    ClientId="client2",
                    ClientName="client2",
                    ClientSecrets=new List<Secret>{ new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes=new List<string>{ "api.weatherforecast", "api.test","api.identity" }
                }
            };
    }
}