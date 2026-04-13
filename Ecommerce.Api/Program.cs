using Ecommerce.Api.Data;
using Ecommerce.Api.Endpoints;
using Ecommerce.Api.Middlewares;
using Ecommerce.Api.Repositories;
using Ecommerce.Api.Repositories.Interfaces;
using Ecommerce.Api.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.AddEcommerceDb();

builder.Services.AddValidatorsFromAssembly(typeof (Program).Assembly, includeInternalTypes: true);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapProductsEndpoints();
app.MapCategoriesEndpoints();

app.MigrateDb();

app.Run();
