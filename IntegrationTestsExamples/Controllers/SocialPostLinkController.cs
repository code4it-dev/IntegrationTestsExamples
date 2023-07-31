using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestsExamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SocialPostLinkController : ControllerBase
    {
        private readonly ISocialLinkParser _parser;
        private readonly ILogger<SocialPostLinkController> _logger;
        private readonly IConfiguration _config;

        public SocialPostLinkController(ISocialLinkParser parser, ILogger<SocialPostLinkController> logger, IConfiguration config)
        {
            _parser = parser;
            _logger = logger;
            _config = config;
        }

        /*
         * Working URLs:
         *
         https://twitter.com/BelloneDavide/status/1682305491785973760

https://www.linkedin.com/posts/bellonedavide_dotnet-activity-7088205072559423488-LIwC?utm_source=share&utm_medium=member_desktop

https://www.instagram.com/p/CjNJohVt1YU/?utm_source=ig_web_copy_link&igshid=MzRlODBiNWFlZA==

         */

        [HttpGet]
        public IActionResult Get([FromQuery] string uri)
        {
            _logger.LogInformation("Received uri {Uri}", uri);
            if (Uri.TryCreate(uri, new UriCreationOptions { }, out Uri _uri))
            {
                var linkInfo = _parser.GetLinkInfo(_uri);
                _logger.LogInformation("Uri {Uri} is of type {Type}", uri, linkInfo.SocialNetworkName);

                var instance = new Instance
                {
                    InstanceName = _config.GetValue<string>("InstanceName"),
                    Info = linkInfo
                };
                return Ok(instance);
            }
            else
            {
                _logger.LogWarning("Uri {Uri} is not a valid Uri", uri);
                return BadRequest();
            }
        }
    }

    public class LinkInfo
    {
        public string SocialNetworkName { get; set; }
        public Uri SourceUrl { get; set; }
        public string Username { get; set; }
        public string Id { get; set; }
    }

    public class Instance
    {
        public string InstanceName { get; set; }
        public LinkInfo Info { get; set; }
    }
}