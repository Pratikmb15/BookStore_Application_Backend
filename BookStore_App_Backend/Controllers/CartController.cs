using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepoLayer.Entity;
using System.Security.Claims;

namespace BookStore_App_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<IActionResult> AddItemToCart(CartItemModel cartModel)
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
                var result = await _cartService.AddItemToCart( userId, cartModel);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Failed to add item to cart" });
                }
                return Ok(new { success = true, message = "Item added to cart successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Failed to add item to cart" });
            }
        }
        [HttpGet]
        public IActionResult GetAllCartItems()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (userId <= 0)
                {
                    return BadRequest(new { success = false, message = "Invalid user ID" });
                }
                var cartItems = _cartService.GetAllCartItems(userId);
                if (cartItems == null || !cartItems.Any())
                {
                    return NotFound(new { success = false, message = "No cart items found" });
                }
                return Ok(new { success = true, message = "Cart items found successfully", data = cartItems });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Failed to retrieve cart items" });
            }
        }
        [HttpGet("{cartItemId}")]

        public  IActionResult GetCartItemByID(int cartItemId)
        {
            try
            {
                if (cartItemId <= 0)
                {
                    return BadRequest(new { success = false, message = "Invalid cart item ID" });
                }
                var CartItem =  _cartService.GetCartItemById(cartItemId);
                if (CartItem == null)
                {
                    return NotFound(new { success = false, message = "Cart item not found" });
                }
                return Ok(new { success = true, message = "Cart item found successfully", data = CartItem });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Failed to retrieve cart item" });
            }
        }
        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, CartItemModel cart)
        {
            try
            {
                if (cartItemId <= 0 || !ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid cart item ID or model" });
                }
                var result = await _cartService.UpdateCartItem(cartItemId, cart);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Failed to update cart item" });
                }
                return Ok(new { success = true, message = "Cart item updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Failed to update cart item" });
            }
        }
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            try
            {
                if (cartItemId <= 0)
                {
                    return BadRequest(new { success = false, message = "Invalid cart item ID" });
                }
                var result = await _cartService.DeleteCartItem(cartItemId);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Failed to delete cart item" });
                }
                return Ok(new { success = true, message = "Cart item deleted successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Failed to delete cart item" });
            }
        }
        [HttpPost("purchase")]
        public IActionResult PurchaseCartItems()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (userId <= 0)
                {
                    return BadRequest(new { success = false, message = "Invalid user ID" });
                }
                var result =(_cartService.PurchaseCartItems(userId));
                 var totalPrice = Convert.ToInt32(result);
                if (totalPrice <= 0)
                {
                    return BadRequest(new { success = false, message = "Failed to purchase cart items" });
                }
                return Ok(new { success = true, message = "Cart items purchased successfully",data=totalPrice });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Failed to purchase cart items" });
            }
        }
    }
}
