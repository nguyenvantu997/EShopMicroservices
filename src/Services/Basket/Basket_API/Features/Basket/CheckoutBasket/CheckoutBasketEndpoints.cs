
using Basket_API.Dtos;
using Basket_API.Features.Basket.DeleteBasket;

namespace Basket_API.Features.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDto Dto);

    public record CheckoutBasketResponse(bool IsSuccess);

    public class CheckoutBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var result = await sender.Send(new CheckoutBasketCommand(request));

                var response = result.Adapt<CheckoutBasketResponse>();

                return Results.Ok(response);
            }).WithName("Checkout Basket")
             .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Checkout Basket")
             .WithDescription("Checkout Basket");
        }
    }
}
