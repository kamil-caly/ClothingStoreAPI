using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Middleware;
using ClothingStoreAPI.Seeders;
using ClothingStoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClothingStoreDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});
builder.Services.AddScoped<ClothingStoreSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IClothingStoreService, ClothingStoreService>();
builder.Services.AddScoped<IStoreReviewService, StoreReviewService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductReviewService, ProductReviewService>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ClothingStoreSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
