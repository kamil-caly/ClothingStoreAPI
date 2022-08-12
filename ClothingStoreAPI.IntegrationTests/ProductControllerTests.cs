using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Update;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ClothingStoreAPI.IntegrationTests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient client;
        private WebApplicationFactory<Program> factory;

        public ProductControllerTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
            this.client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(services =>
                            services
                            .ServiceType == typeof(DbContextOptions<ClothingStoreDbContext>));

                        services.Remove(dbContextOptions);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));

                        services.AddDbContext<ClothingStoreDbContext>(options =>
                            options.UseInMemoryDatabase("ClothingStoreDb"));
                    });
                })
                .CreateClient();
        }

        [Theory]
        [InlineData("PageNumber=1&PageSize=1")]
        [InlineData("PageNumber=1&PageSize=3")]
        [InlineData("PageNumber=1&PageSize=5")]
        [InlineData("PageNumber=1&PageSize=10")]
        [InlineData("PageNumber=1&PageSize=15")]
        public async Task GetAll_WIthQueryParameters_ReturnsOkResult(string queryParams)
        {
            // act

            var response = await client.GetAsync("/Api/ClothingStore/3/Product?" + queryParams);

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("PageNumber=1&PageSize=2")]
        [InlineData("PageNumber=1&PageSize=6")]
        [InlineData("PageNumber=1&PageSize=511")]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetAll_WithInvalidQueryParams_ReturnBadRequest(string queryParams)
        {
            // act

            var response = await client.GetAsync("/Api/ClothingStore/3/Product?" + queryParams);

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateClothingStore_WithValidModel_ReturnsCreatedStatus()
        {
            // arrange 

            var model = new CreateProductDto()
            {
                Name = "testProduct",
                Brand = "test",
                Type = "test",
                Gender = "test",
                Size = "test",
                Price = 22.44M,
                Quantity = 2
            };

            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // act 

            var response = await client.PostAsync("/Api/ClothingStore/3/Product/", httpContent);

            // arrange

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Theory]
        [InlineData("8")]
        [InlineData("7")]
        [InlineData("9")]
        public async Task UpdateClothingStore_WithInValidModel_ReturnsNotFoundStatus(string productId)
        {
            // arrange 

            var updateModel = new UpdateProductDto()
            {
                Name = "testProduct",
                Brand = "test",
                Type = "test",
                Gender = "test",
                Size = "test",
                Price = 22.44M,
                Quantity = 2
            };

            var json = JsonConvert.SerializeObject(updateModel);

            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // act 

            var response = await client.PutAsync("/Api/ClothingStore/3/Product/" + productId, httpContent);

            // arrange

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("88")]
        [InlineData("78")]
        [InlineData("98")]
        public async Task Delete_ForNonExistingclothingStore_ReturnsNotFound(string productId)
        {
            // act

            var response = await client.DeleteAsync("/Api/ClothingStore/3/Product/" + productId);

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
    }
}
