using Catalog_API.Features.Products.GetProducts;

namespace Catalog_API.Features.Products.GetProductById
{
    //public record GetProductByIdRequest();
    public record GetProductByIdResponse(Product product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/product/{id}", async (Guid id, ISender sender) =>
            {
                //var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            }).WithName("GetProductById")
               .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Get Product by id")
               .WithDescription("Get Product by id");
        }
    }
}
