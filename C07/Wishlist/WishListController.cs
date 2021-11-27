using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Wishlist;

[Route("/")]
public class WishListController : ControllerBase
{
    private readonly IWishList _wishList;

    public WishListController(IWishList wishList)
    {
        _wishList = wishList ?? throw new ArgumentNullException(nameof(wishList));
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var items = await _wishList.AllAsync();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody, Required] CreateItem newItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var item = await _wishList.AddOrRefreshAsync(newItem.Name);
        return Created("/", item);
    }

    public class CreateItem
    {
        [Required]
        public string Name { get; set; } = "";
    }
}
