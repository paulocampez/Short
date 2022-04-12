using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace HeyUrl.Urls.Domain.Models
{
    public class Clicks : Identifiable
    {
        [Attr]
        public string Platform { get; set; }
        [Attr]
        public string Browser { get; set; }
        [Attr]
        public DateTime CreatedAt { set; get; }

        [HasOne]
        public Url Url { get; set; }
    }
}
