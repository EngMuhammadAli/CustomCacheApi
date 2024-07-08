using CustomCacheApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CustomCacheApi.ApplicationDb
{
    public class CacheDbContext : DbContext
    {
        public CacheDbContext(DbContextOptions<CacheDbContext> options) : base(options) { }

        public DbSet<CacheEntry> CacheEntries { get; set; }
    }
}
