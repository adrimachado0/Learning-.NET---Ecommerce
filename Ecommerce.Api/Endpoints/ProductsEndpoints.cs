using Ecommerce.Api.Dtos;
using Ecommerce.Api.Services;
using FluentValidation;

namespace Ecommerce.Api.Endpoints;

public static class ProductsEndpoints
{
    private const string GetProductName = "GetProduct";

    public static void MapProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products");

        group.MapGet("/", async (
            [AsParameters] ProductFilterQueryDto filtros,
            IValidator<ProductFilterQueryDto> validator,
            IProductService productService
        ) =>
        {
            var result = await validator.ValidateAsync(filtros);
            if(!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());
            
            var products = await productService.GetPaginatedProductsAsync(filtros);

            return Results.Ok(products);
        });

        group.MapGet("/{id}", async (
            int id, 
            IProductService productService
        ) =>
        {
            var product = await productService.GetProductByIdAsync(id);

            if( product is null )
                return Results.Problem(
                    statusCode: 404,
                    title: "Producto no encontrado",
                    detail: $"No existe un producto con el ID: {id}"
                );

            return Results.Json(product);
        }).WithName(GetProductName);

        group.MapPost("/", async (
            CreateProductDto newProduct,
            IValidator<CreateProductDto> validator,
            ICategoryService categoryService,
            IProductService productService
        ) =>
        {   
            var result = await validator.ValidateAsync(newProduct);
            if( !result.IsValid )
                return Results.ValidationProblem(result.ToDictionary());

            var product = await productService.CreateProductAsync(newProduct);
            
            return Results.CreatedAtRoute(
                GetProductName, 
                new { id = product.Id }, 
                product 
            );
        });

        group.MapPut("/{id}", async (
            int id, 
            UpdateProductDto updatedProduct,
            IValidator<UpdateProductDto> validator,
            IProductService productService
        ) =>
        {
            var result = await validator.ValidateAsync(updatedProduct);
            if( !result.IsValid )
                return Results.ValidationProblem(result.ToDictionary());

            var product = await productService.UpdateProductAsync(id, updatedProduct);

            if( product is null )
                return Results.NotFound();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, IProductService productService) =>
        {
            var isDeleted = await productService.DeleteProductAsync(id);

            if(isDeleted is false)
                return Results.Problem(
                    statusCode: 404,
                    title: "Producto no encontrado",
                    detail: $"No existe un producto con el ID: {id}"
                );

            return Results.NoContent();
        });
    }
}
