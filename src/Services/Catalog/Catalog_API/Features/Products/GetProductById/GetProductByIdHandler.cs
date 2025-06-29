using Catalog_API.Exceptions;

namespace Catalog_API.Features.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IDocumentSession _documentSession;

        public GetProductByIdHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _documentSession.LoadAsync<Product>(request.Id);
            if (product == null)
                throw new ProductNotFoundException(request.Id);

            return new GetProductByIdResult(product);
        }
    }
}
