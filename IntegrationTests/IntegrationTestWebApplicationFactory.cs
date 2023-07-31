using IntegrationTestsExamples;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntegrationTests
{
    public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((host, configurationBuilder) =>
            {
                configurationBuilder.Sources.Clear();

                configurationBuilder.AddInMemoryCollection(
                    new List<KeyValuePair<string, string?>>
                    {
                      new KeyValuePair<string, string?>("InstanceName", "FromTests")
                    });
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<ISocialLinkParser, StubSocialLinkParser>();
                services.AddLogging(builder => builder.ClearProviders().AddConsole().AddDebug());
            });
        }
    }
}