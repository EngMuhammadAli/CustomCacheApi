using CustomCacheApi.ApplicationDb;
using CustomCacheApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomCacheApi.Services
{

    public class CacheService
    {
        private readonly CacheDbContext _context;

        public CacheService(CacheDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetCachedResponseAsync(string key)
        {
            var cacheEntry = await _context.CacheEntries
                .Where(c => c.Key == key && c.CreatedAt > DateTime.UtcNow.AddHours(-2))
                .FirstOrDefaultAsync();

            return cacheEntry?.Response;
        }

        public async Task SetCacheResponseAsync(string key, string response)
        {
            var cacheEntry = new CacheEntry
            {
                Key = key,
                Response = response,
                CreatedAt = DateTime.UtcNow
            };
            _context.CacheEntries.Add(cacheEntry);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveExpiredCacheEntriesAsync()
        {
            var expiredEntries = await _context.CacheEntries
                .Where(c => c.CreatedAt <= DateTime.UtcNow.AddHours(-2))
                .ToListAsync();

            _context.CacheEntries.RemoveRange(expiredEntries);
            await _context.SaveChangesAsync();
        }
    }
}
