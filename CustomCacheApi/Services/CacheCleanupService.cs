namespace CustomCacheApi.Services
{
    public class CacheCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CacheCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var cacheService = scope.ServiceProvider.GetRequiredService<CacheService>();
                    await cacheService.RemoveExpiredCacheEntriesAsync();
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
