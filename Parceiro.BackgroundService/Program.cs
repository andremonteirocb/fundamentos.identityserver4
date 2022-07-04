using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parceiro.BackgroundService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            //var disco = await client.GetDiscoveryDocumentAsync("http://localhost:8080/realms/master/");keycloak
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001"); //identityserver4
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "f33fdd0d183f4f97a07496db0dd8b38b",
                ClientSecret = "8b24e91c4c4846698242141e6224738d",
                Scope = "api_frete.read_only"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"https://localhost:5007/fretes/para/{-23d},{-51}/calcular?altura={10d}&largura={1d}&comprimento={31d}&peso={3}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.ReadKey();
        }

    }
}
