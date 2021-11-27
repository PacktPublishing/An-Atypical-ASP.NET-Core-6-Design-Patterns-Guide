namespace Wishlist
{
    public interface IWishList
    {
        Task<WishListItem> AddOrRefreshAsync(string itemName);
        Task<IEnumerable<WishListItem>> AllAsync();
    }
}
