using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ModelLayer;
using System.Security.Claims;

namespace BookStore_App_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public orderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _orderService.AddOrder(userId);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Failed to add order" });
                }
                return Ok(new { success = true, message = "Order Placed successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = ex.Message});
            }
        }
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (userId <= 0)
                {
                    return BadRequest(new { success = false, message = "Invalid user ID" });
                }
                var response = _orderService.GetAllOrders(userId);
                return Ok(new { success = true, message = "Orders retrieved successfully", data = response });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message =ex.Message});
            }
        }
        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (userId <= 0)
                {
                    return BadRequest(new { success = false, message = "Invalid user ID" });
                }
                var response = _orderService.GetOrderById(userId, orderId);
                return Ok(new { success = true, message = "Order retrieved successfully", data = response });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
