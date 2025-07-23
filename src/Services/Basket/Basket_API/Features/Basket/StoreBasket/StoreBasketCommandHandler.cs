
using Basket_API.Data;

namespace Basket_API.Features.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHanlder<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            var result = await basketRepository.StoreBasketAsync(cart, cancellationToken);

            return new StoreBasketResult(result.UserName);
        }
    }
}
