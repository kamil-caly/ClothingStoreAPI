using ClothingStoreAPI.Services;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Route("/api/ClothingStore/{storeId}/StoreReview")]
    public class StoreReviewController : ControllerBase
    {
        private readonly IStoreReviewService reviewService;

        public StoreReviewController(IStoreReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StoreReviewDto>> Get([FromRoute] int storeId)
        {
            var reviews = reviewService.GetAll(storeId);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
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
        public ActionResult Update([FromRoute] int storeId, [FromRoute] int reviewId, [FromBody] UpdateStoreReviewDto dto)
        {
            reviewService.Update(storeId, reviewId, dto);

            return Ok();
        }

        [HttpDelete]
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
