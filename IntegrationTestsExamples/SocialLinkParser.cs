using IntegrationTestsExamples.Controllers;
using IntegrationTestsExamples.SocialHandlers;

namespace IntegrationTestsExamples
{
    public interface ISocialLinkParser
    {
        LinkInfo GetLinkInfo(Uri postUri);
    }

    public class SocialLinkParser : ISocialLinkParser
    {
        private readonly BaseLinkParserHandler _rootHandler;
        private readonly ILogger<SocialLinkParser> _logger;

        public SocialLinkParser(ISocialLinksFactory factory, ILogger<SocialLinkParser> logger)
        {
            _rootHandler = factory.GetRootParser();
            _logger = logger;
        }

        public LinkInfo GetLinkInfo(Uri postUri)
        {
            _logger.LogInformation("Ready to parse {Url}", postUri);
            return _rootHandler.GetLinkInfo(postUri);
        }
    }
}