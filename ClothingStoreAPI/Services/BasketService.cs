using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Create;
using Microsoft.AspNetCore.Authorization;

namespace ClothingStoreAPI.Services
{
    public class BasketService : IBasketService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;
        private readonly IProductService productService;

        public BasketService(ClothingStoreDbContext dbContext, IMapper mapper,
            ILogger<ClothingStoreService> logger, IAuthorizationService authorizationService,
            IUserContextService userContextService, IProductService productService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
            this.productService = productService;
        }

        public void AddToBasket(CreateOrderDto dto)
        {
            var basket = this.GetOrCreateUserBasket();

            var newOrder = mapper.Map<Order>(dto);
            newOrder.BasketId = basket.Id;
            dbContext.Orders.Add(newOrder);
            
            dbContext.SaveChanges();
        }

        private Basket GetOrCreateUserBasket()
        {
            var userId = userContextService.GetUserId;

            var basket = dbContext
                .Baskets
                .FirstOrDefault(b => b.CreatedById == userId);

            if (basket is null)
            {
                var newBasket = new Basket() { CreatedById = userId};
                dbContext.Baskets.Add(newBasket);
                dbContext.SaveChanges();
                return newBasket;
            }

            return basket;
        }

        public Basket GetExistingUserBasket()
        {
            var userId = userContextService.GetUserId;

            var basket = dbContext
                .Baskets
                .FirstOrDefault(b => b.CreatedById == userId);

            if (basket is null)
            {
                throw new NotFoundException("You have not basket yet.");
            }

            return basket;
        }
    }
}
