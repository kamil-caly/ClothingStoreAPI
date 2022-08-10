using AutoMapper;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Create;
using Microsoft.AspNetCore.Authorization;

namespace ClothingStoreAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ClothingStoreService> logger;
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;
        private readonly IProductService productService;
        private readonly IBasketService basketService;

        public OrderService(ClothingStoreDbContext dbContext, IMapper mapper,
            ILogger<ClothingStoreService> logger, IAuthorizationService authorizationService,
            IUserContextService userContextService, IProductService productService,
            IBasketService basketService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
            this.productService = productService;
            this.basketService = basketService;
        }

        public void AddOrder(int storeId, int productId, int quantity)
        {
            var product = productService.GetProductById(productId, storeId);

            if (product.Quantity - quantity <= 0)
            {
                throw new SoldOutException("Product sold out, change quantity or pick other one.");
            }

            var orderDto = mapper.Map<CreateOrderDto>(product);
            orderDto.ProductQuantity = quantity;

            basketService.AddToBasket(orderDto);
        }

        public void DeleteOrder(int orderId)
        {
            var basket = basketService.GetExistingUserBasket();

            var order = dbContext
                .Orders
                .Where(o => o.BasketId == basket.Id)
                .FirstOrDefault(o => o.Id == orderId);

            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }

            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
        }
    }
}
