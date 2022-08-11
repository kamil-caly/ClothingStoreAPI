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
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClothingStoreAPI.Services
{
   
    public class ProductService : IProductService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IClothingStoreService storeService;
        private readonly ILogger<ProductService> logger;
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;

        public ProductService(ClothingStoreDbContext dbContext, IMapper mapper,
            IClothingStoreService storeService, ILogger<ProductService> logger,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.storeService = storeService;
            this.logger = logger;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
        }

        public int Create(int storeId, CreateProductDto dto)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var product = mapper.Map<Product>(dto);

            product.CreatedById = userContextService.GetUserId;
            product.StoreId = store.Id;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return product.Id;
        }

        public void Delete(int storeId, int productId)
        {
            logger.LogError($"Product with id: {productId} from store with id: {storeId} DELETE ACTION invoked");

            var product = this.GetProductById(productId, storeId);

            var authorizationResult = authorizationService
                .AuthorizeAsync(userContextService.User, product,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You don't have access to someone else's store's product");
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }

        public void DeleteAll(int storeId)
        {
            logger.LogError($"All products from store with id: {storeId} DELETEALL ACTION invoked");

            var store = storeService.GetStoreFromDb(storeId);

            var products = dbContext
                .Products
                .Where(p => p.StoreId == store.Id)
                .ToList();

            if (!products.Any())
            {
                throw new NotFoundAnyItemException($"Cannot find any products for Store Id: {store.Id}");
            }

            var authorizationResult = authorizationService
                .AuthorizeAsync(userContextService.User, store,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You don't have access to someone else's store's products");
            }

            dbContext.RemoveRange(products);
            dbContext.SaveChanges();
        }

        public PageResult<ProductDto> GetAll(int storeId, HttpQuery query)
        {
            var store = storeService.GetStoreFromDb(storeId);

            var baseQuery = dbContext
                .Products
                .Include(p => p.ProductReviews)
                .Where(p => p.StoreId == store.Id && (query.SearchPhraze == null
                    || p.Name.ToLower().Contains(query.SearchPhraze.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var dictionary = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    {nameof(Product.Name), p => p.Name },
                    {nameof(Product.Description), p => p.Description },
                    {nameof(Product.Price), p => p.Price },
                    {nameof(Product.Type), p => p.Type },
                    {nameof(Product.Gender), p => p.Gender },
                    {nameof(Product.Size), p => p.Size }
                };

                var selectedColumn = dictionary[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var products = baseQuery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToList();

            if (!products.Any())
            {
                throw new NotFoundAnyItemException("Cannot find any product for this store.");
            }

            var totalItemsCount = baseQuery.Count();

            var productDtos = mapper.Map<List<ProductDto>>(products);

            var pageResult = new PageResult<ProductDto>(productDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return pageResult;
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

            var authorizationResult = authorizationService
                .AuthorizeAsync(userContextService.User, product,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You don't have access to someone else's product");
            }

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
