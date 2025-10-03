namespace Ordering.Infrastructure.Data.Extensions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(Guid.Parse("11111111-1111-1111-1111-111111111111")), "Alice Johnson", "alice.johnson@example.com"),
                Customer.Create(CustomerId.Of(Guid.Parse("22222222-2222-2222-2222-222222222222")), "Bob Smith", "bob.smith@example.com"),
                Customer.Create(CustomerId.Of(Guid.Parse("33333333-3333-3333-3333-333333333333")), "Charlie Brown", "charlie.brown@example.com"),
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), "Laptop Dell XPS 15", 2000m),
                Product.Create(ProductId.Of(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), "iPhone 15 Pro", 1500m),
                Product.Create(ProductId.Of(Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")), "Samsung Galaxy S24", 1300m),
                Product.Create(ProductId.Of(Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd")), "Sony WH-1000XM5", 400m),
                Product.Create(ProductId.Of(Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee")), "Apple Watch Series 9", 500m)
            };

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var order1 = Order.Create(
                    OrderId.Of(Guid.Parse("99999999-1111-1111-1111-111111111111")),
                    CustomerId.Of(Guid.Parse("11111111-1111-1111-1111-111111111111")), // Alice
                    OrderName.Of("OR001"),
                    Address.Of("Alice", "Johnson", "alice.johnson@example.com", "123 Main St", "USA", "CA", "90001"),
                    Address.Of("Alice", "Johnson", "alice.johnson@example.com", "123 Main St", "USA", "CA", "90001"),
                    Payment.Of("Credit Card", "4111111111111111", "12/26", "123", 1)
                );

                // Add items
                order1.Add(ProductId.Of(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), 1, 2000m); // Laptop
                order1.Add(ProductId.Of(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), 2, 1500m); // iPhone

                var order2 = Order.Create(
                    OrderId.Of(Guid.Parse("99999999-2222-2222-2222-222222222222")),
                    CustomerId.Of(Guid.Parse("22222222-2222-2222-2222-222222222222")), // Bob
                    OrderName.Of("OR002"),
                    Address.Of("Bob", "Smith", "bob.smith@example.com", "456 Park Ave", "USA", "NY", "10001"),
                    Address.Of("Bob", "Smith", "bob.smith@example.com", "456 Park Ave", "USA", "NY", "10001"),
                    Payment.Of("PayPal", "bob.smith@example.com", "12/26", "123", 2)
                );

                order2.Add(ProductId.Of(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), 1, 1500m); // iPhone

                return new List<Order> { order1, order2 };
            }
        }
    }
}
