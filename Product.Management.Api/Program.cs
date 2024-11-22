using Microsoft.EntityFrameworkCore;
using Product.Management.Api.Contracts.Interfaces;
using Product.Management.Api.DatabaseContext;
using Product.Management.Api.Middleware;
using Product.Management.Api.Repositories.Implementation;
using Product.Management.Api.Repositories.Interfaces;
using Product.Management.Api.UnitofWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMemoryCache();
builder.Services.AddDbContext<ProductManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductMgmtConnectionString"));
});

builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("all");

app.UseAuthorization();

app.MapControllers();

app.Run();
