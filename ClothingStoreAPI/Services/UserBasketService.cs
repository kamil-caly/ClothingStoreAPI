using AutoMapper;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Create;
using ClothingStoreModels.Dtos.Dispaly;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreAPI.Services
{
    public class UserBasketService : IUserBasketService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ClothingStoreService> logger;
        private readonly IUserContextService userContextService;

        public UserBasketService(ClothingStoreDbContext dbContext, IMapper mapper,
            ILogger<ClothingStoreService> logger, IUserContextService userContextService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.userContextService = userContextService;
        }

        public void AddToBasket(CreateOrderDto dto, int productId)
        {
            var basket = this.GetOrCreateUserBasket();

            var newOrder = mapper.Map<Order>(dto);
            newOrder.BasketId = basket.Id;
            newOrder.ProductId = productId;

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

        public Basket GetExistingUserBasketForOrderService()
        {
            var userId = userContextService.GetUserId;

            var basket = dbContext
                .Baskets
                .Include(b => b.Orders)
                .FirstOrDefault(b => b.CreatedById == userId);

            if (basket is null)
            {
                throw new NotFoundException("You have not basket yet.");
            }

            return basket;
        }

        public int CreateBasket()
        {
            var user = dbContext
                .Users.FirstOrDefault(u => u.Id == userContextService.GetUserId);

            var basket = dbContext
                .Baskets
                .FirstOrDefault(b => b.CreatedById == user.Id);

            if (basket != null)
            {
                throw new OperationCannotPerformedException("You already have a basket");
            }

            var newBasket = new Basket();
            newBasket.CreatedById = user.Id;

            dbContext.SaveChanges();

            return newBasket.Id;
        }

        public BasketDto GetBasketDto()
        {
            var userId = userContextService.GetUserId;

            var basket = dbContext
                .Baskets
                .Include(b => b.Orders)
                .FirstOrDefault(b => b.CreatedById == userId);

            if (basket is null)
            {
                throw new NotFoundException("You have not basket yet.");
            }

            var basketDto = mapper.Map<BasketDto>(basket);

            return basketDto;
        }

        public void DeleteBasket()
        {
            var user = dbContext
                .Users.FirstOrDefault(u => u.Id == userContextService.GetUserId);

            var basket = dbContext
                .Baskets
                .FirstOrDefault(b => b.CreatedById == user.Id);

            if (basket == null)
            {
                throw new NotFoundException("You have not basket yet");
            }

            logger.LogError($"Basket with id: {basket.Id} DELETE ACTION invoked");

            dbContext.Baskets.Remove(basket);
            dbContext.SaveChanges();
        }
    }
}
