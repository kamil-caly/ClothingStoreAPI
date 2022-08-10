using ClothingStoreAPI.Services;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Route("api/ClothingStore/{storeId}/product/{productId}/productReview")]
    [Authorize]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewService productReviewService;

        public ProductReviewController(IProductReviewService productReviewService)
        {
            this.productReviewService = productReviewService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ProductReviewDto>> GetAll([FromRoute] int storeId, [FromRoute] int productId)
        {
            var productReviews = productReviewService.GetAll(storeId, productId);

            return Ok(productReviews);
        }

        [HttpGet("{productReviewId}")]
        [AllowAnonymous]
        public ActionResult<ProductReviewDto> Get([FromRoute] int storeId, [FromRoute] int productId, [FromRoute] int productReviewId)
        {
            var productReview = productReviewService.GetById(storeId, productId, productReviewId);

            return Ok(productReview);
        }

        [HttpPost]
        public ActionResult Create([FromRoute] int storeId, [FromRoute] int productId, [FromBody] CreateProductReviewDto dto)
        {
            int newProductReviewId = productReviewService.Create(storeId, productId, dto);

            return Created($"api/ClothingStore/{storeId}/product/{productId}/productReview{newProductReviewId}", null);
        }

        [HttpPut("{productReviewId}")]
        [Authorize(Roles = "Manager,Admin,UserPremium")]
        public ActionResult Update([FromRoute] int storeId, [FromRoute] int productId,
            int productReviewId, [FromBody] UpdateProductReviewDto dto)
        {
            productReviewService.Update(storeId, productId, productReviewId, dto);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Manager,Admin")]
        public ActionResult DeleteAll([FromRoute] int storeId, [FromRoute] int productId)
        {
            productReviewService.DeleteAll(storeId, productId);

            return NoContent();
        }

        [HttpDelete("{productReviewId}")]
        public ActionResult Delete([FromRoute] int storeId, [FromRoute] int productId, [FromRoute] int productReviewId)
        {
            productReviewService.Delete(storeId, productId, productReviewId);

            return NoContent();
        }
    }
}
