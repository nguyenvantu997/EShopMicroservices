using Catalog_API.Exceptions;

namespace Catalog_API.Features.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsDelete);
    public class DeleteProductHanler : ICommandHanlder<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IDocumentSession _documentSession;

        public DeleteProductHanler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _documentSession.LoadAsync<Product>(request.Id);
            if (product == null)
                throw new ProductNotFoundException(request.Id);

            _documentSession.Delete(product);
            await _documentSession.SaveChangesAsync();

            return new DeleteProductResult(true);
        }
    }
}
