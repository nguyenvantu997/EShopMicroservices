using Basket_API.Data;
using Discount.Grpc;

namespace Basket_API.Features.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandHandler(
        IBasketRepository basketRepository,
        DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) : ICommandHanlder<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            // communicate with Discount.Grpc and caculate latest prices of product
            await ReduceDiscount(command.Cart, cancellationToken);

            ShoppingCart cart = command.Cart;

            var result = await basketRepository.StoreBasketAsync(cart, cancellationToken);

            return new StoreBasketResult(result.UserName);
        }

        private async Task ReduceDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProtoServiceClient.GetDiscountAsync(
                    new GetDiscountRequest { ProductName = item.ProductName },
                    cancellationToken: cancellationToken);

                item.Price -= coupon.Amount;
            }
        }
    }
}
