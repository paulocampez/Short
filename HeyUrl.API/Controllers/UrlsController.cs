using HeyUrl.Urls.Application.Interface;
using HeyUrl.Urls.Data.Repository;
using HeyUrl.Urls.Domain.Interfaces;
using HeyUrl.Urls.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeyUrl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUrlService _urlService;
        public UrlsController(IUrlRepository urlRepository, IUrlService urlService)
        {
            _urlRepository = urlRepository;
            _urlService = urlService;
        }

        // POST api/<UrlsController>
        [HttpPost]
        public async Task<Url> Post([FromBody] Url model)
        {
            await _urlService.Create(model);
            await _urlService.Update(model);
            return model;
        }
    }
}
