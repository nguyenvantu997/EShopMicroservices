namespace Catalog_API.Features.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsDelete);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            }).WithName("DeleteProduct")
               .Produces<DeleteProductEndpoint>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Delete Product")
               .WithDescription("Delete Product");
        }
    }
}
