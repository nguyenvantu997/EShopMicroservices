﻿namespace Basket_API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
        Task DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default);
    }
}
