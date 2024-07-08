namespace CustomCacheApi.Model
{
    public class CacheEntry
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Response { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
