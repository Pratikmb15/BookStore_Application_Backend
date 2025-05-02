using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Security.Claims;

namespace BookStore_App_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class wishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;
        public wishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }
        [HttpPost]
        public IActionResult AddToWishList(WishListItemModel item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation Failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = _wishListService.AddToWishList(userId,item);
                if (result == null)
                {
                    return NotFound(new { success = false, message = "Book not found" });
                }
                return Ok(new { success = true, message = "Book added to wishlist successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAllWishListItems()
        {
            try
            {
                var wishListItems = _wishListService.GetAllWishListItems(1);
                if (wishListItems == null || wishListItems.Count == 0)
                {
                    return NotFound(new { success = false, message = "No items found in wishlist" });
                }
                return Ok(new { success = true, message = "Wish list items Fetched Successfully", data = wishListItems });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("{wishListItemId}")]
        public IActionResult GetWishListItemById(int wishListItemId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var wishListItem = _wishListService.GetWishListItemById(userId, wishListItemId);
                if (wishListItem == null)
                {
                    return NotFound(new { success = false, message = "No item found in wishlist" });
                }
                return Ok(new { success = true, message = "Wish list item fetched successfully", data = wishListItem });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete("{wishListItemId}")]
        public IActionResult RemoveFromWishList(int wishListItemId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = _wishListService.RemoveFromWishList(userId, wishListItemId);
                if (result == null)
                {
                    return NotFound(new { success = false, message = "No item found in wishlist" });
                }
                return Ok(new { success = true, message = "Item removed from wishlist successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

    }
}
