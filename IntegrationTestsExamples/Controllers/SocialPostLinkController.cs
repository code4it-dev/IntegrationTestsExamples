using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestsExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SocialPostLinkController : ControllerBase
    {
        private readonly ISocialLinkParser _parser;
        private readonly ILogger<SocialPostLinkController> _logger;

        public SocialPostLinkController(ISocialLinkParser parser, ILogger<SocialPostLinkController> logger)
        {
            _parser = parser;
            _logger = logger;
        }

        /*
         https://twitter.com/BelloneDavide/status/1682305491785973760

https://www.linkedin.com/posts/bellonedavide_dotnet-activity-7088205072559423488-LIwC?utm_source=share&utm_medium=member_desktop

https://www.instagram.com/p/CjNJohVt1YU/?utm_source=ig_web_copy_link&igshid=MzRlODBiNWFlZA==

         */

        [HttpGet]
        public LinkInfo Get([FromQuery] Uri uri)
        {
            return _parser.GetLinkInfo(uri);
        }
    }

    public class LinkInfo
    {
        public string SocialNetworkName { get; set; }
        public Uri SourceUrl { get; set; }
        public string Username { get; set; }
        public string Id { get; set; }
    }
}