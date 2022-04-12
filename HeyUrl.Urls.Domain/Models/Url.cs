using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeyUrl.Urls.Domain.Models
{
    public class Url : Identifiable
    {
        [Attr]
        public string ShortUrl { get; set; }
        [Attr]
        public string OriginalUrl { get; set; }
        [Attr]
        public int Count { get; set; }
        [Attr]
        public DateTime CreatedAt { set; get; }

        [HasMany]
        public ICollection<Clicks>? Clicks { get; set; }

    }
}
