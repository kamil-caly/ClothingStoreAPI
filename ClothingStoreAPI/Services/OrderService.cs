using AutoMapper;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Exceptions;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Dispaly;

namespace ClothingStoreAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<OrderService> logger;
        private readonly IBasketService basketService;

        public OrderService(ClothingStoreDbContext dbContext, IMapper mapper,
            ILogger<OrderService> logger, IBasketService basketService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.basketService = basketService;
        }

        public void DeleteAllOrders(int basketId)
        {
            var basket = basketService.GetBasket(basketId);

            var orders = dbContext
                .Orders
                .Where(o => o.BasketId == basket.Id)
                .ToList();

            if (orders is null)
            {
                throw new NotFoundAnyItemException($"Not found any orders for basket with Id: {basketId}");
            }

            dbContext.Orders.RemoveRange(orders);
            dbContext.SaveChanges();
        }

        public void DeleteOrder(int basketId, int orderId)
        {
            var basket = basketService.GetBasket(basketId);

            var order = dbContext
                .Orders
                .Where(o => o.BasketId == basket.Id)
                .FirstOrDefault(o => o.Id == orderId);

            if (order is null)
            {
                throw new NotFoundException($"Not found order for Id: {orderId}");
            }

            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
        }

        public IEnumerable<OrderDto> GetAll(int basketId)
        {
            var basket = basketService.GetBasket(basketId);

            var orders = dbContext
                .Orders
                .Where(o => o.BasketId == basket.Id)
                .ToList();

            if (orders is null)
            {
                throw new NotFoundAnyItemException($"Not found any orders for basket with Id: {basketId}");
            }

            var orderDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

            return orderDtos;
        }

        public OrderDto GetOrder(int basketId, int orderId)
        {
            var basket = basketService.GetBasket(basketId);

            var order = dbContext
                .Orders
                .Where(o => o.BasketId == basket.Id)
                .FirstOrDefault(o => o.Id == orderId);

            if (order is null)
            {
                throw new NotFoundException($"Not found order for Id: {orderId}");
            }

            var orderDto = mapper.Map<OrderDto>(order);

            return orderDto;
        }
    }
}
