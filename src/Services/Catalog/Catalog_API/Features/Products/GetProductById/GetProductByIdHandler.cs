using Catalog_API.Exceptions;

namespace Catalog_API.Features.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetProductByIdHandler> _logger;

        public GetProductByIdHandler(IDocumentSession documentSession, ILogger<GetProductByIdHandler> logger)
        {
            _documentSession = documentSession;
            _logger = logger;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get product query handle called {@query}", request.Id);

            var product = await _documentSession.LoadAsync<Product>(request.Id);
            if (product == null)
                throw new ProductNotFoundException();

            return new GetProductByIdResult(product);
        }
    }
}
