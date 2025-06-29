using Marten.Schema;
using System.Threading.Tasks;

namespace Catalog_API.Data
{
    public class CatalogInitialData : IInitialData
    {
        private List<Category> _categories = new();
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            _categories = await GetPreconfiguredCategories(session);
            session.Store<Category>(_categories);

            var products = GetPreconfiguredProducts();
            session.Store<Product>(products);

            await session.SaveChangesAsync();
        }

        private IEnumerable<Product> GetPreconfiguredProducts()
        {
            var electronicsId = _categories.First(c => c.Name == "Electronics").Id;
            var booksId = _categories.First(c => c.Name == "Books").Id;
            var clothingId = _categories.First(c => c.Name == "Clothing").Id;

            return new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Smartphone", Description = "128GB, OLED display", ImageFile = "smartphone.png", Price = 799.99m, CategoryId = electronicsId },
                new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "15-inch, 16GB RAM", ImageFile = "laptop.png", Price = 1199.99m, CategoryId = electronicsId },
                new Product { Id = Guid.NewGuid(), Name = "Wireless Earbuds", Description = "Noise-cancelling", ImageFile = "earbuds.png", Price = 149.99m, CategoryId = electronicsId },
                new Product { Id = Guid.NewGuid(), Name = "Smartwatch", Description = "Fitness tracking", ImageFile = "smartwatch.png", Price = 199.99m, CategoryId = electronicsId },

                new Product { Id = Guid.NewGuid(), Name = "Mystery Novel", Description = "A gripping mystery", ImageFile = "book.png", Price = 12.99m, CategoryId = booksId },
                new Product { Id = Guid.NewGuid(), Name = "Science Fiction", Description = "Futuristic adventure", ImageFile = "scifi.png", Price = 15.99m, CategoryId = booksId },
                new Product { Id = Guid.NewGuid(), Name = "Cookbook", Description = "100+ delicious recipes", ImageFile = "cookbook.png", Price = 22.50m, CategoryId = booksId },
                new Product { Id = Guid.NewGuid(), Name = "Self-Help Guide", Description = "Improve your mindset", ImageFile = "selfhelp.png", Price = 18.75m, CategoryId = booksId },

                new Product { Id = Guid.NewGuid(), Name = "Denim Jacket", Description = "Unisex, classic fit", ImageFile = "jacket.png", Price = 59.99m, CategoryId = clothingId },
                new Product { Id = Guid.NewGuid(), Name = "T-Shirt", Description = "100% cotton, white", ImageFile = "tshirt.png", Price = 19.99m, CategoryId = clothingId },
                new Product { Id = Guid.NewGuid(), Name = "Sneakers", Description = "Comfortable and stylish", ImageFile = "sneakers.png", Price = 89.99m, CategoryId = clothingId },
                new Product { Id = Guid.NewGuid(), Name = "Backpack", Description = "Water-resistant, 20L", ImageFile = "backpack.png", Price = 49.99m, CategoryId = clothingId }
            };
        }


        private async Task<List<Category>> GetPreconfiguredCategories(IDocumentSession session)
        {
            if (await session.Query<Category>().AnyAsync())
                return (await session.Query<Category>().ToListAsync()).ToList();

            return new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Electronics",
                    Description = "Devices, gadgets and accessories"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Books",
                    Description = "Fiction and non-fiction books"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Clothing",
                    Description = "Men's and Women's Apparel"
                }
            };
        }

    }
}
