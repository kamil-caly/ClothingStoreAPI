using AutoMapper;
using ClothingStoreAPI.Authorization;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.AspNetCore.Authorization;

namespace ClothingStoreAPI.Services
{
    
    public class StoreReviewService : IStoreReviewService
    {
        private readonly IMapper mapper;
        private readonly ClothingStoreDbContext dbContext;
        private readonly IClothingStoreService storeService;
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;

        public StoreReviewService(IMapper mapper, ClothingStoreDbContext dbContext,
            IClothingStoreService storeService, IAuthorizationService authorizationService,
            IUserContextService userContextService)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.storeService = storeService;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
        }

        public int Create(int storeId, CreateStoreReviewDto dto)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var ReviewEntity = mapper.Map<StoreReview>(dto);

            ReviewEntity.CreatedById = userContextService.GetUserId;
            ReviewEntity.StoreId = store.Id;
            dbContext.StoreReviews.Add(ReviewEntity);
            dbContext.SaveChanges();

            return ReviewEntity.Id;
        }

        public void Delete(int storeId, int reviewId)
        {
            var review = this.GetReviewById(reviewId, storeId);

            var authorizationResult = authorizationService
                .AuthorizeAsync(userContextService.User, review,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You don't have access to someone else's review");
            }

            dbContext.StoreReviews.Remove(review);
            dbContext.SaveChanges();
        }

        public void DeleteAll(int storeId)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var reviews = dbContext
                .StoreReviews
                .Where(r => r.StoreId == store.Id)
                .ToList();

            if (!reviews.Any())
            {
                throw new NotFoundAnyItemException($"Cannot find any reviews for Store Id = {storeId}");
            }

            var authorizationResult = authorizationService
                .AuthorizeAsync(userContextService.User, reviews,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You don't have access to someone else's review");
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
                throw new NotFoundAnyItemException($"Cannot found any reviews for this Store Id = {storeId}");
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

            var authorizationResult = authorizationService
                .AuthorizeAsync(userContextService.User, review,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You don't have access to someone else's review");
            }

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
