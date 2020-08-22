using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ClientCredentialsTest();
            await ResourceOwnerPasswordTest();

            Console.ReadKey();
        }

        static async Task ClientCredentialsTest()
        {
            var httpClient = new HttpClient();

            // get DiscoveryDocument
            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // get TokenResponse
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client1",
                ClientSecret = "secret",
                Scope = "api.identity api.test"
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine(tokenResponse.Json);

            // call Api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var apiResponse = await apiClient.GetAsync("http://localhost:5001/api/identity/get");
            if (!apiResponse.IsSuccessStatusCode) 
            {
                Console.WriteLine(apiResponse.ReasonPhrase);
                return;
            }
            Console.WriteLine(await apiResponse.Content.ReadAsStringAsync());
        }

        static async Task ResourceOwnerPasswordTest()
        {
            var httpClient = new HttpClient();

            // get DiscoveryDocument
            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // get TokenResponse
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client2",
                ClientSecret = "secret",
                Scope = "api.identity api.test",

                UserName="hongyan",
                Password="123456"
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine(tokenResponse.Json);

            // call Api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var apiResponse = await apiClient.GetAsync("http://localhost:5001/api/identity/get");
            if (!apiResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(apiResponse.ReasonPhrase);
                return;
            }
            Console.WriteLine(await apiResponse.Content.ReadAsStringAsync());
        }
    }
}
