using ClothingStoreAPI.Exceptions;

namespace ClothingStoreAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException notFoundStore)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundStore.Message);
            }
            catch (NotFoundAnyItemException notFoundAnyStores)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundAnyStores.Message);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unexpected exception");
            }
        }
    }
}
