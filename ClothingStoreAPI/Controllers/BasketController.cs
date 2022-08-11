using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Dispaly;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    [Route("api/Basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BasketDto>> Get()
        {
            var basketsDto = basketService.GetAll();

            return Ok(basketsDto);
        }

        [HttpGet("{basketId}")]
        public ActionResult<BasketDto> Get([FromRoute] int basketId)
        {
            var basketDto = basketService.Get(basketId);

            return Ok(basketDto);
        }

        [HttpDelete]
        public ActionResult DeleteBaskets()
        {
            basketService.DeleteAll();

            return NoContent(); 
        }

        [HttpDelete("{basketId}")]
        public ActionResult DeleteBaskets([FromRoute] int basketId)
        {
            basketService.Delete(basketId);

            return NoContent();
        }
    }
}
