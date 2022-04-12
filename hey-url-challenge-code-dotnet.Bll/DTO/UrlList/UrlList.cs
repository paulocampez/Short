using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Bll.DTO.UrlList
{
   
    public class UrlListLinks
    {
        public string self { get; set; }
        public string first { get; set; }
        public string next { get; set; }
        public string related { get; set; }
    }

    public class UrlListAttributes
    {
        public string shortUrl { get; set; }
        public string originalUrl { get; set; }
        public int count { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class UrlListClicks
    {
        public UrlListLinks links { get; set; }
    }

    public class UrlListRelationships
    {
        public UrlListClicks clicks { get; set; }
    }

    public class UrlListData
    {
        public string type { get; set; }
        public string id { get; set; }
        public UrlListAttributes attributes { get; set; }
        public UrlListRelationships relationships { get; set; }
        public UrlListLinks links { get; set; }
    }

    public class UrlList
    {
        public UrlListLinks links { get; set; }
        public List<UrlListData> data { get; set; }
    }


}
