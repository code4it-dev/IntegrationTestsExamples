using IntegrationTestsExamples;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class SocialLinkParserTests : IDisposable
    {
        protected IServiceScope _scope;
        protected SocialLinkParser _sut;
        private IntegrationTestWebApplicationFactory _factory;

        [OneTimeSetUp]
        public void OneTimeSetup() => _factory = new IntegrationTestWebApplicationFactory();

        [SetUp]
        public void Setup()
        {
            _scope = _factory.Services.CreateScope();
            _sut = _scope.ServiceProvider.GetRequiredService<SocialLinkParser>();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null;
            _scope.Dispose();
        }

        public void Dispose() => _factory?.Dispose();

        [Test]
        public async Task Should_GetInvalidLink_When_UrlIsNotRecognized()
        {
            var invalidUri = new Uri("https://website.that.I.dont.know/postname");
            var info = _sut.GetLinkInfo(invalidUri);
            Assert.That(info.SocialNetworkName, Is.EqualTo("Unknown"));
        }

        [Test]
        public async Task Should_GetTwitterLink_When_UrlIsTwitter()
        {
            var invalidUri = new Uri("https://twitter.com/BelloneDavide/status/1682305491785973760");
            var info = _sut.GetLinkInfo(invalidUri);
            Assert.That(info.SocialNetworkName, Is.EqualTo("Twitter"));
            Assert.That(info.Username, Is.EqualTo("BelloneDavide"));
            Assert.That(info.Id, Is.EqualTo("1682305491785973760"));
        }
    }
}