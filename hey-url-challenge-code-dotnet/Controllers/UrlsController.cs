using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Bll.Interface;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;
using static hey_url_challenge_code_dotnet.Bll.UrlBLL;
using PagedList;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private readonly IUrlBLL _urlBLL;

        public UrlsController(IUrlBLL urlBLL, ILogger<UrlsController> logger, IBrowserDetector browserDetector)
        {
            _urlBLL = urlBLL;
            this.browserDetector = browserDetector;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 0)
        {
            var model = new HomeViewModel();

            var lstUrl = await _urlBLL.GetUrlList(page);

            List<Url> lstUrlsModel = new List<Url>();

            lstUrl.Item1.data.ForEach(x =>
            {
                lstUrlsModel.Add(new hey_url_challenge_code_dotnet.Models.Url()
                {
                    ShortUrl = x.attributes.shortUrl,
                    Count = _urlBLL.GetUrlClicks(int.Parse(x.id)).Result.data.Count(),
                    OriginalUrl = x.attributes.originalUrl,
                    CreatedAt = x.attributes.createdAt,
                    Id = int.Parse(x.id)
                });

            });
            model.Urls = lstUrlsModel;
            model.urlPage = page == 0 ? 1 : page;
            model.absolutePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            if (lstUrl.Item2)
            {
                model.maxPage = true;
            }



            model.NewUrl = new();
            return View(model);
        }

        [Route("/{url}")]
        public async Task<IActionResult> Visit(string url)
        {
            var website = await _urlBLL.GetUrlByShortUrl(url);
            if (website.data.Count == 0)
                return NotFound();
            else
            {
                var idUrl = int.Parse(_urlBLL.GetUrlByShortUrl(RouteData.Values["url"].ToString()).Result.data[0].id);
                var success = await _urlBLL.CreateClick(idUrl, this.browserDetector.Browser.OS, this.browserDetector.Browser.Name);
                return Redirect(website.data.First().attributes.originalUrl);
            }
                
        }

        [Route("urls/{url}")]
        public async Task<IActionResult> Show(int idUrl, bool saveClick = true)
        {
            if (saveClick)
            {
                if (idUrl == 0)
                {
                    idUrl =  int.Parse(_urlBLL.GetUrlByShortUrl(RouteData.Values["url"].ToString()).Result.data[0].id);
                }
                var success = await _urlBLL.CreateClick(idUrl, this.browserDetector.Browser.OS, this.browserDetector.Browser.Name);
            }

            var model = await _urlBLL.GetUrl(idUrl);
            var clicks = await _urlBLL.GetUrlClicks(idUrl);
            var clicksModel = clicks.data.Where(x => x.type == "clicks");
            var clicksDay = clicksModel.Where(x => x.attributes.createdAt.Date == DateTime.Now.Date);
            var clicksBrowse = clicksModel.Select(x => x.attributes.browser).Distinct();
            var browsers = clicksModel.GroupBy(x => x.attributes.browser)
            .Where(x => x.Count() > 0)
            .Select(x => new { Value = x.Key, Count = x.Count() })
              .ToDictionary(x => x.Value, x => x.Count);
            var platform = clicksModel.GroupBy(x => x.attributes.platform)
             .Where(x => x.Count() > 0)
             .Select(x => new { Value = x.Key, Count = x.Count() })
             .ToDictionary(x => x.Value, x => x.Count);
            var daily = clicksModel.Where(x => DateTime.Now.Month == x.attributes.createdAt.Month).GroupBy(x => x.attributes.createdAt.Date)
                .Where(x => x.Count() > 0)
                .Select(x => new { Value = x.Key.Day.ToString(), Count = x.Count() })
                .ToDictionary(x => x.Value, x => x.Count);



            ShowViewModel modelReturn = new ShowViewModel()
            {
                Url = new Url { OriginalUrl = model.data.attributes.originalUrl, CreatedAt = model.data.attributes.createdAt, Id = int.Parse(model.data.id), ShortUrl = model.data.attributes.shortUrl },
                DailyClicks = daily,
                BrowseClicks = browsers,
                PlatformClicks = platform
            };
            return View(modelReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeViewModel url)
        {
            Url newUrl = new Url();

            Uri uriResult;
            bool result = Uri.TryCreate(url.NewUrl.OriginalUrl, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result)
            {
                newUrl = await _urlBLL.CreateUrl(url.NewUrl);
                url.NewUrl = newUrl;
                return RedirectToAction("Show", new { idUrl = url.NewUrl.Id, saveClick = false });
            }
            else
            {
                TempData["Notice"] = "Invalid Url";
                return RedirectToAction("Index");
            }


        }
    }
}