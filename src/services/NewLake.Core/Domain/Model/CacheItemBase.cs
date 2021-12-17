using System;

namespace NewLake.Core.Domain.Model
{
    public abstract class CacheItemBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime LastUpdated { get; set; }
    }


    public class CacheItem : CacheItemBase
    {
    }
}
