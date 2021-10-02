using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wishlist
{
    public interface IWishList
    {
        Task<WishListItem> AddOrRefreshAsync(string itemName);
        Task<IEnumerable<WishListItem>> AllAsync();
    }
}
