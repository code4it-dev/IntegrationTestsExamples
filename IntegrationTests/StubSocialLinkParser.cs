using IntegrationTestsExamples;
using IntegrationTestsExamples.Controllers;

namespace IntegrationTests
{
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