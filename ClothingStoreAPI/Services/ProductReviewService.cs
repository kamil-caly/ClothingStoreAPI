using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;

namespace ClothingStoreAPI.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IClothingStoreService storeService;
        private readonly IProductService productService;

        public ProductReviewService(ClothingStoreDbContext dbContext,
            IMapper mapper,
            IClothingStoreService storeService,
            IProductService productService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.storeService = storeService;
            this.productService = productService;
        }

        public int Create(int storeId, int productId, CreateProductReviewDto dto)
        {
            var product = productService.GetProductById(productId, storeId);

            var productReview = mapper.Map<ProductReview>(dto);

            productReview.ProductId = product.Id;
            dbContext.Add(productReview);
            dbContext.SaveChanges();

            return productReview.Id;
        }

        public void Delete(int storeId, int productId, int productReviewId)
        {
            var productReview = this.GetProductReviewById(storeId, productId, productReviewId);

            dbContext.Remove(productReview);
            dbContext.SaveChanges();
        }

        public void DeleteAll(int storeId, int productId)
        {
            var product = productService.GetProductById(productId, storeId);

            var productReviews = dbContext
                .ProductReviews
                .Where(r => r.ProductId == product.Id)
                .ToList();

            if (!productReviews.Any())
            {
                throw new NotFoundAnyItemException($"Cannot find any product review for this product Id = {product.Id}.");
            }

            dbContext.RemoveRange(productReviews);
            dbContext.SaveChanges();
        }

        public IEnumerable<ProductReviewDto> GetAll(int storeId, int productId)
        {
            var product = productService.GetProductById(productId, storeId);

            var productReviews = dbContext
                .ProductReviews
                .Where(p => p.ProductId == product.Id)
                .ToList();

            if (!productReviews.Any())
            {
                throw new NotFoundAnyItemException("Cannot find any product review for this product.");
            }

            var productReviewsDto = mapper.Map<List<ProductReviewDto>>(productReviews);
            return productReviewsDto;
        }

        public ProductReviewDto GetById(int storeId, int productId, int productReviewId)
        {
            var productReview = this.GetProductReviewById(storeId, productId, productReviewId);

            var productReviewDto = mapper.Map <ProductReviewDto>(productReview);
            return productReviewDto;
        }

        public void Update(int storeId, int productId, int productReviewId, UpdateProductReviewDto dto)
        {
            var productReview = this.GetProductReviewById(storeId, productId, productReviewId);

            productReview = mapper.Map(dto, productReview);
            dbContext.SaveChanges();
        }

        private ProductReview GetProductReviewById(int storeId, int productId, int productReviewId)
        {
            var product = productService.GetProductById(productId, storeId);

            var productReview = dbContext
                .ProductReviews
                .FirstOrDefault(r => r.Id == productReviewId && r.ProductId == product.Id);

            if (productReview is null)
            {
                throw new NotFoundException("Product review not found");
            }

            return productReview;
        }
        
    }
}
