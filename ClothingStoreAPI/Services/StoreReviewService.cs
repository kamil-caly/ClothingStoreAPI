using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;

namespace ClothingStoreAPI.Services
{
    public interface IStoreReviewService
    {
        IEnumerable<StoreReviewDto> GetAll(int storeId);
        StoreReviewDto GetById(int storeId, int reviewId);
        int Create(int storeId, CreateStoreReviewDto dto);

        void Update(int storeId, int reviewId, UpdateStoreReviewDto dto);
        void DeleteAll(int storeId);
        void Delete(int storeId, int reviewId);
    }
    public class StoreReviewService : IStoreReviewService
    {
        private readonly IMapper mapper;
        private readonly ClothingStoreDbContext dbContext;
        private readonly IClothingStoreService storeService;

        public StoreReviewService(IMapper mapper, ClothingStoreDbContext dbContext, IClothingStoreService storeService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.storeService = storeService;
        }

        public int Create(int storeId, CreateStoreReviewDto dto)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var ReviewEntity = mapper.Map<StoreReview>(dto);

            ReviewEntity.StoreId = store.Id;
            dbContext.StoreReviews.Add(ReviewEntity);
            dbContext.SaveChanges();

            return ReviewEntity.Id;
        }

        public void Delete(int storeId, int reviewId)
        {
            var review = this.GetReviewById(reviewId, storeId);

            dbContext.StoreReviews.Remove(review);
            dbContext.SaveChanges();
        }

        public void DeleteAll(int storeId)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var reviews = dbContext
                .StoreReviews
                .Where(r => r.StoreId == store.Id);

            if (!reviews.Any())
            {
                throw new NotFoundAnyItemException($"Cannot find any reviews for Store Id = {store.Id}");
            }

            dbContext.StoreReviews.RemoveRange(reviews);
            dbContext.SaveChanges();
        }

        public IEnumerable<StoreReviewDto> GetAll(int storeId)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var reviews = dbContext
                .StoreReviews
                .Where(r => r.StoreId == store.Id);

            if (!reviews.Any())
            {
                throw new NotFoundAnyItemException($"Cannot found any reviews for this Store Id = {store.Id}");
            }

            var reviewsDto = mapper.Map<List<StoreReviewDto>>(reviews);
            return reviewsDto;
        }

        public StoreReviewDto GetById(int storeId, int reviewId)
        {
            var review = this.GetReviewById(reviewId, storeId);

            var reviewDto = mapper.Map<StoreReviewDto>(review);
            return reviewDto;
        }

        public void Update(int storeId, int reviewId, UpdateStoreReviewDto dto)
        {
            var review = this.GetReviewById(reviewId, storeId);

            review = mapper.Map(dto, review);
            dbContext.SaveChanges();
        }

        private StoreReview GetReviewById(int reviewId, int storeId)
        {
            var review = dbContext
                .StoreReviews
                .FirstOrDefault(s => s.Id == reviewId);

            if (review is null || review.StoreId != storeId)
            {
                throw new NotFoundException("Store Review not found");
            }

            return review;
        }
        
    }

    
}
