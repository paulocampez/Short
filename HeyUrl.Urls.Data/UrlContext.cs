using HeyUrl.Urls.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HeyUrl.Urls.Data
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options)
        {

        }

        public DbSet<Url> Urls { get; set; }
        public DbSet<Clicks> Clicks { get; set; }

    }
}
