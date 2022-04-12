using HeyUrl.Urls.Domain.Models;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HeyUrl.API.Controllers
{
    
    public class UrlController : JsonApiController<Url>
    {
        public UrlController(IJsonApiOptions options,
                 ILoggerFactory loggerFactory,
                 IResourceService<Url> resourceService) : base(options, loggerFactory, resourceService)
        {

        }
    }
}
