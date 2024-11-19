using System.ComponentModel.DataAnnotations;
using Catalog.API.Mappers;
using Catalog.API.Request.Product;
using Catalog.Domain.Models;
using Catalog.Infraestructure;
using Catalog.Services.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Domain.Dtos;
using Shared.Domain.Dtos.Response;

namespace Catalog.API.Apis;

public static class ProducApi
{
    public static IEndpointRouteBuilder MapCatalogApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/products/");

        // Routes for querying catalog items.
        api.MapPost("/products", CreateItem);
        api.MapGet("/products", GetAllItems);
        api.MapGet("/products/{id:int}", GetItemById);
        api.MapPut("/products/{id:int}", UpdateItem);
        api.MapDelete("/products/{id:int}", DeleteItemById);

        return app;
    }

    public static async Task<Results<Ok<PaginatedData<Product>>, BadRequest<BadRequestResponse>>> GetAllItems(
        [AsParameters] PaginationRequest paginationRequest,
        [FromServices] IValidator<PaginationRequest> validator,
        [FromServices] ProductService services)
    {

        var validationResult = await validator.ValidateAsync(paginationRequest);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(new BadRequestResponse("BadRequest", validationResult.ToDictionary()));
        }

        var data =  await services.GetAsync(paginationRequest.PageIndex, paginationRequest.PageSize);

        return TypedResults.Ok(data);
    }

    public static async Task<Results<Ok<Product>, NotFound, BadRequest<string>>> GetItemById(
        [FromServices] ProductService services,
        [FromRoute] int id)
    {
        if (id <= 0)
            return TypedResults.BadRequest("Id is not valid.");

        var item = await services.GetByIdAsync(id);

        if (item == null)
            return TypedResults.NotFound();

        return TypedResults.Ok(item);
    }

    public static async Task<Results<Ok<Product>, BadRequest<BadRequestResponse>>> CreateItem(
        [FromServices] ProductService services,
        [FromServices] IValidator<CreateProductRequest> validator,
        CreateProductRequest request)
    {
        if (request == null)
            throw new ArgumentNullException();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(new BadRequestResponse("BadRequest", validationResult.ToDictionary()));
        }

        var product = request.ToProduct();
        var response = await services.SaveAsync(product);

        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<Product>, NotFound<string>>> UpdateItem(
        [FromServices] ProductService services,
        [FromRoute] int id,
        [FromBody] ProductUpdateRequest request)
    {
        var productUpdate = request.ToUpdateProductOptions();

        var product = await services.UpdateAsync(id, productUpdate);

        return TypedResults.Ok(product);
    }


    public static async Task<Results<NoContent, NotFound>> DeleteItemById(
        [FromServices] ProductService services,
        [FromRoute] int id)
    {
        await services.DeleteAsync(id);

        return TypedResults.NoContent();
    }
}
