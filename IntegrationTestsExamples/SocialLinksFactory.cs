using IntegrationTestsExamples.SocialHandlers;

namespace IntegrationTestsExamples
{
    public interface ISocialLinksFactory
    {
        BaseLinkParserHandler GetRootParser();
    }

    public class SocialLinksFactory : ISocialLinksFactory
    {
        private readonly IServiceProvider _services;

        public SocialLinksFactory(IServiceProvider services)
        {
            _services = services;
        }

        public BaseLinkParserHandler GetRootParser()
        {
            var invalid = _services.GetRequiredService<InvalidPostHandler>();
            var linkedIn = _services.GetRequiredService<LinkedInHandler>();
            var twitter = _services.GetRequiredService<TwitterHandler>();

            twitter.SetNext(linkedIn);
            linkedIn.SetNext(invalid);

            return twitter;
        }
    }
}