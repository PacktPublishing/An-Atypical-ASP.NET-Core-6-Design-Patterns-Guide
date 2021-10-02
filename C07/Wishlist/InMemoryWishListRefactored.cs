using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wishlist
{
    public class InMemoryWishListRefactored : IWishList
    {
        private readonly InMemoryWishListOptions _options;
        private readonly Dictionary<string, InternalItem> _items;

        public InMemoryWishListRefactored(InMemoryWishListOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _items = new Dictionary<string, InternalItem>();
        }

        public Task<WishListItem> AddOrRefreshAsync(string itemName)
        {
            RemoveExpiredItems();
            return ItemExists(itemName)
                ? UpdateItem(itemName)
                : CreateItem(itemName)
            ;
        }

        public Task<IEnumerable<WishListItem>> AllAsync()
        {
            RemoveExpiredItems();
            var items = _items
                .Select(x => Map(x.Key, x.Value))
                .OrderByDescending(x => x.Count)
                .AsEnumerable()
            ;
            return Task.FromResult(items);
        }

        private DateTimeOffset GetExpirationTime()
        {
            return _options.SystemClock.UtcNow.AddSeconds(_options.ExpirationInSeconds);
        }

        private Task<WishListItem> CreateItem(string itemName)
        {
            var expirationTime = GetExpirationTime();
            var item = new InternalItem
            {
                Count = 1,
                Expiration = expirationTime
            };
            _items.Add(itemName, item);
            var wishlistItem = Map(itemName, item);
            return Task.FromResult(wishlistItem);
        }

        private Task<WishListItem> UpdateItem(string itemName)
        {
            var expirationTime = GetExpirationTime();
            var item = _items[itemName];
            item.Count++;
            item.Expiration = expirationTime;
            var wishlistItem = Map(itemName, item);
            return Task.FromResult(wishlistItem);
        }

        private static WishListItem Map(string itemName, InternalItem item)
        {
            return new WishListItem
            {
                Name = itemName,
                Count = item.Count,
                Expiration = item.Expiration
            };
        }

        private bool ItemExists(string itemName)
        {
            return _items.ContainsKey(itemName);
        }

        private void RemoveExpiredItems()
        {
            _items
                .Where(x => x.Value.Expiration < _options.SystemClock.UtcNow)
                .Select(x => x.Key)
                .ToList()
                .ForEach(key => _items.Remove(key))
            ;
        }

        private class InternalItem
        {
            public int Count { get; set; }
            public DateTimeOffset Expiration { get; set; }
        }
    }
}
