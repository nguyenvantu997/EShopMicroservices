using Catalog_API.Exceptions;

namespace Catalog_API.Features.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsDelete);
    public class DeleteProductHanler : ICommandHanlder<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<DeleteProductHanler> _logger;

        public DeleteProductHanler(IDocumentSession documentSession, ILogger<DeleteProductHanler> logger)
        {
            _documentSession = documentSession;
            _logger = logger;
        }
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete product command handle called {@query}", request.Id);

            var product = await _documentSession.LoadAsync<Product>(request.Id);
            if (product == null)
                throw new ProductNotFoundException(request.Id);

            _documentSession.Delete(product);
            await _documentSession.SaveChangesAsync();

            return new DeleteProductResult(true);
        }
    }
}
