using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Dispaly;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/UserBasket")]
    public class UserBasketController : ControllerBase
    {
        private readonly IUserBasketService basketService;

        public UserBasketController(IUserBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpPost("Create")]
        public ActionResult Create()
        {
            int basketId = basketService.CreateBasket();

            return Created("$api/Basket/Create/{basketId}", null);
        }

        [HttpGet("Get")]
        public ActionResult<BasketDto> Get()
        {
            BasketDto basketDto = basketService.GetBasketDto();

            return Ok(basketDto);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete()
        {
            basketService.DeleteBasket();

            return NoContent();
        }
    }
}
