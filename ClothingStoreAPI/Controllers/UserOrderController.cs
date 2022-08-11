﻿using ClothingStoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Userorder")]
    public class UserOrderController : ControllerBase
    {
        private readonly IUserOrderService orderService;

        public UserOrderController(IUserOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("addToBasket/{storeId}/{productId}")]
        public ActionResult AddOrderToBasket([FromRoute] int storeId, [FromRoute] int productId, [FromQuery] int quantity)
        {
            orderService.AddOrder(storeId, productId, quantity);

            return Ok("Order successfully added to basket.");
        }

        [HttpDelete("deleteOrder/{orderId}")]
        public ActionResult DeleteOrder([FromRoute] int orderId)
        {
            orderService.DeleteOrder(orderId);

            return NoContent();
        }

        [HttpPut("buyOrder/{orderId}")]
        public ActionResult BuyOrder([FromRoute] int orderId)
        {
            orderService.BuyOrder(orderId);

            return Ok("Product bought.");
        }

        [HttpPut("updateQuantity/{orderId}")]
        public ActionResult UpdateOrder([FromRoute] int orderId, [FromQuery] int quantity)
        {
            orderService.UpdateOrderQuantity(orderId, quantity);

            return Ok();
        }
    }
}