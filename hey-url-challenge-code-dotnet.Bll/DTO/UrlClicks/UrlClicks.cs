using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Bll.DTO.UrlClicks
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Links
    {
        public string self { get; set; }
        public string first { get; set; }
        public string related { get; set; }
    }

    public class Attributes
    {
        public string platform { get; set; }
        public string browser { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class UrlClick
    {
        public Links links { get; set; }
    }

    public class Relationships
    {
        public UrlClick url { get; set; }
    }

    public class Datum
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes attributes { get; set; }
        public Relationships relationships { get; set; }
        public Links links { get; set; }
    }

    public class UrlClicksViewModel
    {
        public Links links { get; set; }
        public List<Datum> data { get; set; }
    }


}
