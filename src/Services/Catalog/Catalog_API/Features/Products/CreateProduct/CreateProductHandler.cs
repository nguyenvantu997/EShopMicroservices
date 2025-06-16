namespace Catalog_API.Features.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductHandler : ICommandHanlder<CreateProductCommand, CreateProductResult>
    {
        private IDocumentSession _session;
        public CreateProductHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price
            };

            _session.Store(product);
            await _session.SaveChangesAsync();

            return new CreateProductResult(product.Id);

            //throw new NotImplementedException();
        }
    }
}
