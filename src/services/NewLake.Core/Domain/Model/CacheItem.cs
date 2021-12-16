using System;

namespace NewLake.Core.Domain.Model
{
    public class CacheItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
