using hey_url_challenge_code_dotnet.Bll.DTO.UrlClicks;
using hey_url_challenge_code_dotnet.Bll.DTO.UrlList;
using hey_url_challenge_code_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static hey_url_challenge_code_dotnet.Bll.UrlBLL;

namespace hey_url_challenge_code_dotnet.Bll.Interface
{
    public interface IUrlBLL
    {
        Task<Url> CreateUrl(Url url);
        Task<UrlViewModel> GetUrl(int urlId);
        Task<bool> CreateClick(int urlId, string os, string name);
        Task<UrlClicksViewModel> GetUrlClicks(int urlId);
        Task<(UrlList, bool)> GetUrlList(int page);
        Task<UrlList> GetUrlByShortUrl(string shortUrl);
    }
}
