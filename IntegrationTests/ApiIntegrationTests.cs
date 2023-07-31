using IntegrationTestsExamples;
using IntegrationTestsExamples.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class ApiIntegrationTests : IDisposable
    {
        private IntegrationTestWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetup() => _factory = new IntegrationTestWebApplicationFactory();

        [SetUp]
        public void Setup() => _client = _factory.CreateClient();

        public void Dispose() => _factory?.Dispose();

        [Test]
        public async Task Should_ReturnHttp200_When_UrlIsValid()
        {
            string inputUrl = "https://twitter.com/BelloneDavide/status/1682305491785973760";

            var result = await _client.GetAsync($"SocialPostLink?uri={inputUrl}");

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Should_ReturnBadRequest_When_UrlIsNotValid()
        {
            string inputUrl = "invalid-url";

            var result = await _client.GetAsync($"/SocialPostLink?uri={inputUrl}");

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Should_ReadInstanceNameFromSettings()
        {
            string inputUrl = "https://twitter.com/BelloneDavide/status/1682305491785973760";

            var result = await _client.GetFromJsonAsync<Instance>($"/SocialPostLink?uri={inputUrl}");

            Assert.That(result.InstanceName, Is.EqualTo("FromTests"));
        }

        [Test]
        public async Task Should_UseStubName()
        {
            string inputUrl = "https://twitter.com/BelloneDavide/status/1682305491785973760";

            var result = await _client.GetFromJsonAsync<Instance>($"/SocialPostLink?uri={inputUrl}");

            Assert.That(result.Info.SocialNetworkName, Is.EqualTo("test from stub"));
        }

        [Test]
        public async Task Should_ResolveDependency()
        {
            using (var _scope = _factory.Services.CreateScope())
            {
                var service = _scope.ServiceProvider.GetRequiredService<SocialLinkParser>();
                Assert.That(service, Is.Not.Null);
                Assert.That(service, Is.AssignableTo<SocialLinkParser>());
            }
        }
    }

    public class StubSocialLinkParser : ISocialLinkParser
    {
        public LinkInfo GetLinkInfo(Uri postUri) => new LinkInfo
        {
            SocialNetworkName = "test from stub",
            Id = "test id",
            SourceUrl = postUri,
            Username = "test username"
        };
    }
}