using ClothingStoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/UserOrder")]
    public class UserOrderController : ControllerBase
    {
        private readonly IUserOrderService orderService;

        public UserOrderController(IUserOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("AddToBasket/{storeId}/{productId}")]
        public ActionResult AddOrderToBasket([FromRoute] int storeId, [FromRoute] int productId, [FromQuery] int quantity)
        {
            orderService.AddOrder(storeId, productId, quantity);

            return Ok("Order successfully added to basket.");
        }

        [HttpDelete("DeleteOrder/{orderId}")]
        public ActionResult DeleteOrder([FromRoute] int orderId)
        {
            orderService.DeleteOrder(orderId);

            return NoContent();
        }

        [HttpPut("BuyOrder/{orderId}")]
        public ActionResult BuyOrder([FromRoute] int orderId)
        {
            orderService.BuyOrder(orderId);

            return Ok("Product bought.");
        }

        [HttpPut("UpdateQuantity/{orderId}")]
        public ActionResult UpdateOrder([FromRoute] int orderId, [FromQuery] int quantity)
        {
            orderService.UpdateOrderQuantity(orderId, quantity);

            return Ok();
        }
    }
}
