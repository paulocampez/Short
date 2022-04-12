using HeyUrl.Urls.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyUrl.Urls.Application.Interface
{
    public interface IUrlService
    {
        Task EncryptUrl(Url url);
        Task Create(Url url);
        Task Update(Url url);
    }
}
