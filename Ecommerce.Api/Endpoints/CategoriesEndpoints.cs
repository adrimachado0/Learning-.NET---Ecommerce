using Ecommerce.Api.Data;
using Ecommerce.Api.Dtos;
using Ecommerce.Api.Models;
using Ecommerce.Api.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoriesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/categories");

        group.MapGet("/", async (
            ICategoryService categoryService
        ) =>
        {
            return await categoryService.GetCategoriesAsync();
        });

        group.MapPost("/", async (
            CreateCategoryDto newCategory,
            IValidator<CreateCategoryDto> validator,
            ICategoryService categoryService
        ) =>
        {
            var result = await validator.ValidateAsync(newCategory);
            if (!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());

            var category = await categoryService.CreateCategoryAsync(newCategory);

            return Results.Ok(category);
        });

        group.MapPut("{id}", async (
            int id, 
            UpdateCategoryDto updatedCategory,
            IValidator<UpdateCategoryDto> validator,
            ICategoryService categoryService
        ) =>
        {
            var result = await validator.ValidateAsync(updatedCategory);
            if (!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());

            var category = await categoryService.UpdateCategoryAsync(id, updatedCategory);
            if(category is null)
                return Results.NotFound();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, ICategoryService categoryService) =>
        {
            var isDeleted = await categoryService.DeleteCategoryByIdAsync(id);

            if(isDeleted is false)
                return Results.Problem(
                    statusCode: 404,
                    title: "Categoría no encontrada",
                    detail: $"No existe una categoría con el ID: {id}"
                );

            return Results.NoContent();
        });
    }
}