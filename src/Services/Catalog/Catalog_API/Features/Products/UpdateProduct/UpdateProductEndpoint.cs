namespace Catalog_API.Features.Products.UpdateProduct
{
    public record UpdateProductRequest(string Name, Guid CategoryId, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccesss);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductRequest request, ISender sender) =>
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                    return Results.BadRequest("Mismatched ID in route and payload.");

                var command = new UpdateProductCommand(
                 id,
                 request.Name,
                 request.CategoryId,
                 request.Description,
                 request.ImageFile,
                 request.Price
             );

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
