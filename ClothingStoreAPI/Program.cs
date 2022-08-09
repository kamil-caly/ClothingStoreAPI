using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Middleware;
using ClothingStoreAPI.Seeders;
using ClothingStoreAPI.Services;
using ClothingStoreAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
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
builder.Services.AddScoped<UserRoleSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IClothingStoreService, ClothingStoreService>();
builder.Services.AddScoped<IStoreReviewService, StoreReviewService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductReviewService, ProductReviewService>();

// nlog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var storeSeeder = scope.ServiceProvider.GetRequiredService<ClothingStoreSeeder>();
storeSeeder.Seed();

var userRoleSeeder = scope.ServiceProvider.GetRequiredService<UserRoleSeeder>();
userRoleSeeder.Seed();

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
