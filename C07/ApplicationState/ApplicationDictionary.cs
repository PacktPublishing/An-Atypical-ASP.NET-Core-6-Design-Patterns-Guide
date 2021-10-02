using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace ApplicationState
{
    public class ApplicationDictionary : IApplicationState
    {
        private readonly Dictionary<string, object> _memoryCache = new Dictionary<string, object>();

        public TItem Get<TItem>(string key)
        {
            if (!Has<TItem>(key))
            {
                return default;
            }
            return (TItem)_memoryCache[key];
        }

        public bool Has<TItem>(string key)
        {
            return _memoryCache.ContainsKey(key) && _memoryCache[key] is TItem;
        }

        public void Set<TItem>(string key, TItem value)
        {
            _memoryCache[key] = value;
        }
    }
}
