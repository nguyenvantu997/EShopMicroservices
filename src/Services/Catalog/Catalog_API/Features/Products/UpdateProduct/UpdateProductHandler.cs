using Catalog_API.Exceptions;

namespace Catalog_API.Features.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, Guid CategoryId, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccesss);
    public class UpdateProductHandler : ICommandHanlder<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IDocumentSession _session;
        public UpdateProductHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(command.Id);
            if (product == null)
                throw new ProductNotFoundException(command.Id);

            product.Name = command.Name;
            product.CategoryId = command.CategoryId;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            _session.Update(product);
            await _session.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
    }
}
