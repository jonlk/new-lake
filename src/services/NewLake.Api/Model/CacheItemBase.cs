using System;

namespace NewLake.Api.Model
{
    public abstract class CacheItemBase
    {
        public DateTime LastUpdated { get; set; }
        public string Key { get; set; }
        public string PreviousValue { get; set; }
        public string Value { get; set; }
    }


    public class CacheItem : CacheItemBase
    {
        public Organization Organization { get; set; }
    }
}
