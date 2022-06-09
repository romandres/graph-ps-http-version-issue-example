using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace GraphPowerShellHttpVersionIssue
{
    internal class GraphHelper
    {
        private readonly IEnumerable<string> scopes = new List<string>()
        {
            "https://graph.microsoft.com/.default"
        };

        private readonly IConfidentialClientApplication clientApp;

        public GraphHelper(string tenantId, string clientId, string clientSecret)
        {
            clientApp = CreateClientApp(tenantId, clientId, clientSecret);
        }

        public async Task<GraphServiceClient> CreateGraphServiceClientAsync()
        {
            var authenticationResult = await clientApp
                    .AcquireTokenForClient(scopes)
                    .ExecuteAsync()
                    .ConfigureAwait(false);

            var graphServiceClient = new GraphServiceClient(
                    "https://graph.microsoft.com/v1.0",
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                    new DelegateAuthenticationProvider(async (requestMessage) =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
                    {
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticationResult.AccessToken);
                    })
                );

            return graphServiceClient;
        }

        private IConfidentialClientApplication CreateClientApp(string tenantId, string clientId, string clientSecret)
        {
            var clientAppBuilder = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithAuthority(AadAuthorityAudience.AzureAdMyOrg)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret);

            return clientAppBuilder.Build();
        }
    }
}
