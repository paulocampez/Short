using HeyUrl.Urls.Domain.Models;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HeyUrl.API.Controllers
{
    
    public class ClicksController : JsonApiController<Clicks>
    {
        // GET: ClicksController

        public ClicksController(IJsonApiOptions options,
                ILoggerFactory loggerFactory,
                IResourceService<Clicks> resourceService) : base(options, loggerFactory, resourceService)
        {

        }

    }
}
