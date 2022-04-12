using HeyUrl.Urls.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyUrl.Urls.Domain.Interfaces
{
    public interface IUrlRepository
    {
        Task<List<Url>> ListAllAsync();
        Task<Url> Add(Url url);
        Task Update(Url url);
    }
}
