using ClothingStoreAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [Route("api/ClothingStore")]
    [ApiController]
    public class ClothingStoreController : ControllerBase
    {
        private readonly IStoreService storeService;

        public ClothingStoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClothingStoreDto>> GetAll()
        {
            var StoreDtos = storeService.GetAll();

            return Ok(StoreDtos);
        }
    }
}
