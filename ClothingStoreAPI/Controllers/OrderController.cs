using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Dispaly;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    [Route("api/Order/{basketId}")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> Get([FromRoute] int basketId)
        {
            var orderDtos = orderService.GetAll(basketId);

            return Ok(orderDtos);
        }

        [HttpGet("{orderId}")]
        public ActionResult<OrderDto> Get([FromRoute] int basketId, [FromRoute] int orderId)
        {
            var orderDto = orderService.GetOrder(basketId, orderId);

            return Ok(orderDto);
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int basketId)
        {
            orderService.DeleteAllOrders(basketId);

            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public ActionResult Delete([FromRoute] int basketId, [FromRoute] int orderId)
        {
            orderService.DeleteOrder(basketId, orderId);

            return NoContent();
        }
    }
}
