using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreAPI.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll(int storeId);
        ProductDto GetById(int storeId, int productId);
        int Create(int storeId, CreateProductDto dto);

        void Update(int storeId, int productId, UpdateProductDto dto);
        void DeleteAll(int storeId);
        void Delete(int storeId, int productId);
        Product GetProductById(int productId, int storeId);
    }
    public class ProductService : IProductService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IClothingStoreService storeService;

        public ProductService(ClothingStoreDbContext dbContext, IMapper mapper, IClothingStoreService storeService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.storeService = storeService;
        }

        public int Create(int storeId, CreateProductDto dto)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var product = mapper.Map<Product>(dto);

            product.StoreId = store.Id;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return product.Id;
        }

        public void Delete(int storeId, int productId)
        {
            var product = this.GetProductById(productId, storeId);

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }

        public void DeleteAll(int storeId)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var products = dbContext
                .Products
                .Where(p => p.StoreId == store.Id)
                .ToList();

            if (!products.Any())
            {
                throw new NotFoundAnyItemException($"Cannot find any products for Store Id: {store.Id}");
            }

            dbContext.RemoveRange(products);
            dbContext.SaveChanges();
        }

        public IEnumerable<ProductDto> GetAll(int storeId)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var products = dbContext
                .Products
                .Include(p => p.ProductReviews)
                .Where(p => p.StoreId == store.Id)
                .ToList();

            if (!products.Any())
            {
                throw new NotFoundAnyItemException("Cannot find any product for this store.");
            }

            var productsDtos = mapper.Map<List<ProductDto>>(products);
            return productsDtos;
        }

        public ProductDto GetById(int storeId, int productId)
        {
            var product = this.GetProductById(productId, storeId);

            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }

        public void Update(int storeId, int productId, UpdateProductDto dto)
        {
            var product = this.GetProductById(productId, storeId);

            product = mapper.Map(dto, product);
            dbContext.SaveChanges();
        }
        public Product GetProductById(int productId, int storeId)
        {
            var product = dbContext
                .Products
                .Include(p => p.ProductReviews)
                .FirstOrDefault(p => p.Id == productId);

            if (product is null || product.StoreId != storeId)
            {
                throw new NotFoundException("Product not found");
            }

            return product;
        }
    }
}
