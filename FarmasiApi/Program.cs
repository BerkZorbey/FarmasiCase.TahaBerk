using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapper;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Persistance.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<MongoDbSetting>(builder.Configuration.GetSection("MongoDb/Product"));

builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
             ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisDb")));

builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
