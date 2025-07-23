
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket_API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        private static string KEY = "basket";
        public async Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var key = GetCacheKey(userName);

            await basketRepository.DeleteBasketAsync(userName, cancellationToken);

            await cache.RemoveAsync(key, cancellationToken);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var key = GetCacheKey(userName);
            var cachedBasket = await cache.GetStringAsync(key, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

            var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);
            await cache.SetStringAsync(key, JsonSerializer.Serialize(basket), cancellationToken);

            return await basketRepository.GetBasketAsync(userName, cancellationToken);
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            var key = GetCacheKey(basket.UserName);

            await basketRepository.StoreBasketAsync(basket, cancellationToken);

            await cache.SetStringAsync(key, JsonSerializer.Serialize(basket), cancellationToken);

            return await basketRepository.StoreBasketAsync(basket, cancellationToken);
        }

        private static string GetCacheKey(string userName) => $"{userName}_{KEY}";
    }
}
