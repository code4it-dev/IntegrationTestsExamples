using IntegrationTestsExamples;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

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
                        new KeyValuePair<string, string?>("myKey", "myValue")
                    });
            });
        }
    }

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}