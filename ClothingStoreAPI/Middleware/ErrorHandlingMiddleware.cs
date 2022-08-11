using ClothingStoreAPI.Exceptions;

namespace ClothingStoreAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (OperationCannotPerformedException operationException)
            {
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync(operationException.Message);
            }
            catch (WrongParameterException wrongParameter)
            {
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync(wrongParameter.Message);
            }
            catch (ForbidException forbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbidException.Message);
            }
            catch(NotFoundException notFoundStore)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundStore.Message);
            }
            catch(NotFoundAnyItemException notFoundAnyStores)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundAnyStores.Message);
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unexpected exception");
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
