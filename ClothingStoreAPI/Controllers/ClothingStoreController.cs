using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Services;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [Route("api/ClothingStore")]
    [ApiController]
    public class ClothingStoreController : ControllerBase
    {
        private readonly IClothingStoreService storeService;

        public ClothingStoreController(IClothingStoreService storeService)
        {
            this.storeService = storeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClothingStoreDto>> GetAll()
        {
            var storeDtos = storeService.GetAll();

            return Ok(storeDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ClothingStoreDto> Get([FromRoute] int id)
        {
            var storeDto = storeService.GetById(id);

            return Ok(storeDto);
        }

        [HttpPost]
        public ActionResult CreateStore([FromBody] CreateClothingStoreDto dto)
        {
            var id = storeService.Create(dto);

            return Created($"/api/ClothingStore/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStore([FromRoute] int id)
        {
            storeService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStore([FromRoute] int id, [FromBody] UpdateClothingStoreDto dto)
        {
            storeService.Update(dto, id);

            return Ok();
        }
    }
}
