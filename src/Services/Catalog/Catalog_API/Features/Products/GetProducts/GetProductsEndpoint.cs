﻿namespace Catalog_API.Features.Products.GetProducts
{
    public record GetProductsRequest(Guid? CategoryId, int? PageNumber = 1, int? PageSize = 10, bool IsGetAll = false);
    public record GetProductsResponse(IEnumerable<Product> Products, long TotalItem, bool HasNextPage);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            }).WithName("GetProducts")
             .Produces<GetProductsResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Products")
             .WithDescription("Get Products");
        }
    }
}
