using HeyUrl.Urls.Domain.Interfaces;
using HeyUrl.Urls.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyUrl.Urls.Data.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlContext _context;
        public UrlRepository(UrlContext context)
        {
            _context = context;
        }

        public async Task<List<Url>> ListAllAsync()
        {
            return await _context.Urls.ToListAsync();
        }

        public async Task<Url> Add(Url url)
        {
            _context.Urls.Add(url);
            await _context.SaveChangesAsync();
            return url;
        }

        public async Task Update(Url url)
        {
            _context.Update(url);
            await _context.SaveChangesAsync();
        }
    }
}
