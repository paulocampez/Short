using hey_url_challenge_code_dotnet.Bll.Interface;
using hey_url_challenge_code_dotnet.Models;
using RestSharp;
using System;
using System.Threading.Tasks;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Serialization.Objects;
using Newtonsoft.Json;
using JsonApiSerializer;
using hey_url_challenge_code_dotnet.Bll.DTO.UrlClicks;
using hey_url_challenge_code_dotnet.Bll.DTO.UrlList;

namespace hey_url_challenge_code_dotnet.Bll
{
    public class UrlBLL : IUrlBLL
    {
        private string _api;
        public UrlBLL()
        {
            _api = Environment.GetEnvironmentVariable("API_URL");
        }

        public async Task<(UrlList, bool)> GetUrlList(int page)
        {
            if (page < 1)
                page = 1;

            var client = new RestClient(_api+$"urls?page%5Bsize%5D=10&page%5Bnumber%5D={page}");
            var request = new RestRequest();

            request.AddHeader("Accept", "application/vnd.api+json");
            var response = await client.GetAsync(request);

            UrlList url = JsonConvert.DeserializeObject<UrlList>(response.Content);

            bool haveMorePages = url.data.Count == 10;

            return (url, haveMorePages);
        }

        public async Task<bool> CreateClick(int urlId, string os, string name)
        {
            var clickModel = new ClickViewModel()
            {
                data = new ClickDataRoot()
                {
                    type = "clicks",
                    attributes = new ClickAttributes()
                    {
                        createdAt = DateTime.Now,
                        platform = os,
                        browser = name
                    },
                    relationships = new ClickRelationships()
                    {
                        url = new ClickUrl()
                        {
                            data = new ClickData()
                            {
                                type = "urls",
                                id = urlId.ToString()
                            }
                        }
                    }
                }
            };
            var json = JsonConvert.SerializeObject(clickModel);
            var client = new RestClient(_api+"clicks");
            var request = new RestRequest().AddJsonBody(clickModel);
            request.AddHeader("Content-Type", "application/vnd.api+json");
            request.AddHeader("Accept", "application/vnd.api+json");
            var response = await client.PostAsync(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<Url> CreateUrl(Url url)
        {
            url.CreatedAt = DateTime.Now;
            var client = new RestClient(_api+"api/Urls");
            var request = new RestRequest().AddJsonBody(url);
            var response = await client.PostAsync<Url>(request);

            return response;
        }

        public async Task<UrlList> GetUrlByShortUrl(string shortUrl)
        {
            var client = new RestClient(_api+$"urls?filter=contains(shortUrl,'{shortUrl}')");
            var request = new RestRequest();
            //
            request.AddHeader("Accept", "application/vnd.api+json");
            var response = await client.GetAsync(request);

            UrlList url = JsonConvert.DeserializeObject<UrlList>(response.Content);

            return url;
        }

        public async Task<UrlViewModel> GetUrl(int urlId)
        {
            var client = new RestClient(_api+$"urls/{urlId}");
            var request = new RestRequest();
            //
            request.AddHeader("Accept", "application/vnd.api+json");
            var response = await client.GetAsync(request);

            UrlViewModel url = JsonConvert.DeserializeObject<UrlViewModel>(response.Content);

            return url;

        }

        public async Task<UrlClicksViewModel> GetUrlClicks(int urlId)
        {

            var client = new RestClient(_api+$"urls/{urlId}/clicks?page%5Bsize%5D=1999");
            var request = new RestRequest();
            //
            request.AddHeader("Accept", "application/vnd.api+json");
            var response = await client.GetAsync(request);

            UrlClicksViewModel url = JsonConvert.DeserializeObject<UrlClicksViewModel>(response.Content);

            return url;
        }


        public class Links
        {
            public string self { get; set; }
            public string related { get; set; }
        }

        public class Attributes
        {
            public string shortUrl { get; set; }
            public string originalUrl { get; set; }
            public int count { get; set; }
            public DateTime createdAt { get; set; }
        }

        public class Clicks
        {
            public Links links { get; set; }
        }

        public class Relationships
        {
            public Clicks clicks { get; set; }
        }

        public class Data
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes attributes { get; set; }
            public Relationships relationships { get; set; }
            public Links links { get; set; }
        }

        public class UrlViewModel
        {
            public Links links { get; set; }
            public Data data { get; set; }
        }



        public class ClickAttributes
        {
            public string platform { get; set; }
            public DateTime createdAt { get; set; }
            public string browser { get; set; }
        }

        public class ClickData
        {
            public string id { get; set; }
            public string type { get; set; }

        }

        public class ClickUrl
        {
            public ClickData data { get; set; }
        }

        public class ClickRelationships
        {
            public ClickUrl url { get; set; }
        }

        public class ClickViewModel
        {
            public ClickDataRoot data { get; set; }
        }
        public class ClickDataRoot
        {
            public string type { get; set; }
            public ClickAttributes attributes { get; set; }
            public ClickRelationships relationships { get; set; }

        }



    }
}
