using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [Route("Api/ClothingStore")]
    [ApiController]
    [Authorize]
    public class ClothingStoreController : ControllerBase
    {
        private readonly IClothingStoreService storeService;

        public ClothingStoreController(IClothingStoreService storeService)
        {
            this.storeService = storeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ClothingStoreDto>> GetAll([FromQuery] HttpQuery query)
        {
            var storeDtos = storeService.GetAll(query);

            return Ok(storeDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<ClothingStoreDto> Get([FromRoute] int id)
        {
            var storeDto = storeService.GetById(id);

            return Ok(storeDto);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public ActionResult CreateStore([FromBody] CreateClothingStoreDto dto)
        {
            var id = storeService.Create(dto);

            return Created($"/Api/ClothingStore/{id}", null);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public ActionResult DeleteStore([FromRoute] int id)
        {
            storeService.Delete(id);

            return NoContent();
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public ActionResult UpdateStore([FromRoute] int id, [FromBody] UpdateClothingStoreDto dto)
        {
            storeService.Update(dto, id);

            return Ok();
        }
    }
}
