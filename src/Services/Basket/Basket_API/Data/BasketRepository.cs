using Basket_API.Exceptions;
using Marten;

namespace Basket_API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);

            await session.SaveChangesAsync(cancellationToken);

            return basket;

        }
    }
}
