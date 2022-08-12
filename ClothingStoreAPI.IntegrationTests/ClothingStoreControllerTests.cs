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
    public class ClothingStoreControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient client;
        private WebApplicationFactory<Program> factory;

        public ClothingStoreControllerTests(WebApplicationFactory<Program> factory)
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

            var response = await client.GetAsync("/Api/ClothingStore?" + queryParams);

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

            var response = await client.GetAsync("/Api/ClothingStore?" + queryParams);

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateClothingStore_WithValidModel_ReturnsCreatedStatus()
        {
            // arrange 

            var model = new CreateClothingStoreDto()
            {
                Name = "testStore",
                ContactNumber = "333",
                ContactEmail = "testEmail",
                Incame = 12.33M,
                OwnerContactEmail = "owernContact@Email",
                OwnerContactNumber = "444",
                Country = "Poland",
                City = "Kraków",
                Street = "Cieńka"
            };

            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // act 

            var response = await client.PostAsync("Api/ClothingStore", httpContent);

            // arrange

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Theory]
        [InlineData("8")]
        [InlineData("7")]
        [InlineData("9")]
        public async Task UpdateClothingStore_WithInValidModel_ReturnsNotFoundStatus(string storeId)
        {
            // arrange 

            var updateModel = new UpdateClothingStoreDto()
            {
                Name = "testStore2",
                ContactNumber = "333444",
                ContactEmail = "testEmail2",
                Incame = 455.33M,
            };

            var json = JsonConvert.SerializeObject(updateModel);

            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // act 

            var response = await client.PutAsync("/Api/ClothingStore/" + storeId, httpContent);

            // arrange

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("88")]
        [InlineData("78")]
        [InlineData("98")]
        public async Task Delete_ForNonExistingclothingStore_ReturnsNotFound(string storeId)
        {
            // act

            var response = await client.DeleteAsync("/Api/ClothingStore/" + storeId);

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
    }
}
