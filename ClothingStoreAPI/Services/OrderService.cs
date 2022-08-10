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

            basketService.AddToBasket(orderDto, product.Id);
        }

        public void BuyOrder(int orderId)
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

            if (order.IsBought == true)
            {
                throw new CannotBuyProductException("The order has already been completed.");
            }

            var productInStore = dbContext
                .Products
                .FirstOrDefault(p => p.Id == order.ProductId);

            if (productInStore is null)
            {
                throw new NotFoundException("Product in Clothing store not found");
            }

            var productInStoreQuantity = productInStore.Quantity;

            var user = dbContext.Users.FirstOrDefault(u => u.Id == userContextService.GetUserId);

            var userMoney = user.Money;

            if (order.ProductQuantity > productInStoreQuantity
                || userMoney < order.ProductQuantity * order.ProductPrice)
            {
                throw new CannotBuyProductException("Cannot buy because product quantity is less than in order" +
                    "or you hav not enaught money.");
            }

            order.IsBought = true;
            user.Money -= order.ProductQuantity * order.ProductPrice;
            productInStore.Quantity -= order.ProductQuantity;
            dbContext.SaveChanges();
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
