using IntegrationTestsExamples.Controllers;

namespace IntegrationTestsExamples
{
    namespace SocialHandlers
    {
        public abstract class BaseLinkParserHandler
        {
            protected BaseLinkParserHandler _next;

            public abstract LinkInfo GetLinkInfo(Uri postUri);

            public virtual void SetNext(BaseLinkParserHandler next) => _next = next;

            protected abstract bool CanHandle(Uri postUri);
        }

        internal class TwitterHandler : BaseLinkParserHandler
        {
            private readonly ILogger<TwitterHandler> _logger;

            public TwitterHandler(ILogger<TwitterHandler> logger)
            {
                _logger = logger;
            }

            public override LinkInfo GetLinkInfo(Uri postUri)
            {
                if (!CanHandle(postUri))
                    return _next.GetLinkInfo(postUri);

                _logger.LogInformation("Url is for Twitter");

                var parts = postUri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);

                return new LinkInfo
                {
                    SourceUrl = postUri,
                    SocialNetworkName = "Twitter",
                    Id = parts[^1],
                    Username = parts[0]
                };
            }

            protected override bool CanHandle(Uri postUri)
            {
                return postUri.Host.Contains("twitter");
            }
        }

        internal class LinkedInHandler : BaseLinkParserHandler
        {
            private readonly ILogger<LinkedInHandler> _logger;

            public LinkedInHandler(ILogger<LinkedInHandler> logger)
            {
                _logger = logger;
            }

            public override LinkInfo GetLinkInfo(Uri postUri)
            {
                if (!CanHandle(postUri))
                    return _next.GetLinkInfo(postUri);

                _logger.LogInformation("Url is for LinkedIn");

                var parts = postUri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                var postFullId = parts[^1];
                var name = postFullId.Split('_').First();
                var id = postFullId.Split('-')[^2];

                return new LinkInfo
                {
                    SourceUrl = postUri,
                    SocialNetworkName = "LinkedIn",
                    Id = id,
                    Username = name
                };
            }

            protected override bool CanHandle(Uri postUri)
            {
                return postUri.Host.Contains("linkedin.com");
            }
        }

        internal class InvalidPostHandler : BaseLinkParserHandler
        {
            private readonly ILogger<InvalidPostHandler> _logger;

            public InvalidPostHandler(ILogger<InvalidPostHandler> logger)
            {
                _logger = logger;
                _next = null;
            }

            public override LinkInfo GetLinkInfo(Uri postUri)
            {
                _logger.LogInformation("Url cannot be recognized");
                return new LinkInfo
                {
                    SourceUrl = postUri,
                    SocialNetworkName = "Unknown",
                    Id = "Unknown",
                    Username = "Unknown"
                };
            }

            public override void SetNext(BaseLinkParserHandler next) => _next = null;

            protected override bool CanHandle(Uri postUri)
            {
                return true;
            }
        }
    }
}