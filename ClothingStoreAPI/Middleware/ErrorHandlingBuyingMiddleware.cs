using ClothingStoreAPI.Exceptions;

namespace ClothingStoreAPI.Middleware
{
    public class ErrorHandlingBuyingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (CannotBuyProductException cannotBuy)
            {
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync(cannotBuy.Message);
            }
            catch (SoldOutException soldOut)
            {
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync(soldOut.Message);
            }
            catch (TooLittleMoneyException littleMoney)
            {
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync(littleMoney.Message);
            }
        }
    }
}
