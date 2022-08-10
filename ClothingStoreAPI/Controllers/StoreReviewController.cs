using ClothingStoreAPI.Services;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Route("/api/ClothingStore/{storeId}/StoreReview")]
    [Authorize]
    public class StoreReviewController : ControllerBase
    {
        private readonly IStoreReviewService reviewService;

        public StoreReviewController(IStoreReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<StoreReviewDto>> Get([FromRoute] int storeId)
        {
            var reviews = reviewService.GetAll(storeId);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [AllowAnonymous]
        public ActionResult<StoreReviewDto> Get([FromRoute] int storeId, [FromRoute] int reviewId)
        {
            var review = reviewService.GetById(storeId, reviewId);

            return Ok(review);
        }

        [HttpPost]
        public ActionResult Create([FromRoute] int storeId, [FromBody] CreateStoreReviewDto dto)
        {
            int newReviewId = reviewService.Create(storeId, dto);

            return Created($"/api/ClothingStore/{storeId}/StoreReview/{newReviewId}", null);
        }

        [HttpPut("{reviewId}")]
        [Authorize(Roles = "Manager,Admin,UserPremium")]
        public ActionResult Update([FromRoute] int storeId, [FromRoute] int reviewId, [FromBody] UpdateStoreReviewDto dto)
        {
            reviewService.Update(storeId, reviewId, dto);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Manager,Admin")]
        public ActionResult DeleteReviews([FromRoute] int storeId)
        {
            reviewService.DeleteAll(storeId);

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        public ActionResult DeleteSingleReview([FromRoute] int storeId, [FromRoute] int reviewId)
        {
            reviewService.Delete(storeId, reviewId);

            return NoContent();
        }
    }
}
