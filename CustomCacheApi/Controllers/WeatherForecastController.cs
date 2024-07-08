using CustomCacheApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomCacheApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly CacheService _cacheService;

        public WeatherForecastController(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string location)
        {
            var cacheKey = $"WeatherForecast_{location}";
            var cachedResponse = await _cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                return Ok(new { Source = "Cache", Data = cachedResponse });
            }

            // Simulate fetching data from an external API
            var response = $"Weather data for {location} at {DateTime.UtcNow}";

            await _cacheService.SetCacheResponseAsync(cacheKey, response);

            return Ok(new { Source = "API", Data = response });
        }
    }
}
