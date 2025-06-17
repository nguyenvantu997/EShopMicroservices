using Catalog_API.Features.Products.GetProducts;

namespace Catalog_API.Features.Categories.GetCategories
{
    //public record GetCategoriesRequest();
    public record GetCategoriesResponse(IEnumerable<Category> Categories);
    public class GetCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/categories", async (ISender sender) =>
            {
                //var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(new GetCategoriesQuery());

                var response = result.Adapt<GetCategoriesResponse>();

                return Results.Ok(response);
            }).WithName("GetCategories")
             .Produces<GetCategoriesResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Categories")
             .WithDescription("Get Categories");
        }
    }
}
