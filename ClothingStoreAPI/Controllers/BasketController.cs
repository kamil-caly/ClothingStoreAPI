using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Dispaly;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
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
