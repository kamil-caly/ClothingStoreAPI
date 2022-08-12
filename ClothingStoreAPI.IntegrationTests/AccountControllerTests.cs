using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Create;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClothingStoreAPI.IntegrationTests
{
    
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient client;
        private Mock<IAccountService> accountServiceMock = new Mock<IAccountService>();

        public AccountControllerTests(WebApplicationFactory<Program> factory)
        {
            client = factory
                  .WithWebHostBuilder(builder =>
                  {
                      builder.ConfigureServices(services =>
                      {
                          var dbContextOptions = services
                              .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ClothingStoreDbContext>));

                          services.Remove(dbContextOptions);

                          services.AddSingleton<IAccountService>(accountServiceMock.Object);


                          services
                           .AddDbContext<ClothingStoreDbContext>(options => options.UseInMemoryDatabase("ClothingStoreDb"));

                      });
                  })
                .CreateClient();
        }

        [Fact]
        public async Task RegisterUser_ForValidModel_ReturnsOk()
        {
            // arrange

            var registerUser = new RegisterUserDto()
            {
                Email = "test@test.com",
                Password = "password123",
                ConfirmPassword = "password123"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            // act

            var response = await client.PostAsync("/Api/Account/Register", httpContent);

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterUser_ForInvalidModel_ReturnsBadRequest()
        {
            // arrange

            var registerUser = new RegisterUserDto()
            {
                Password = "password123",
                ConfirmPassword = "123"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            // act

            var response = await client.PostAsync("/Api/Account/Register", httpContent);

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
    
}
