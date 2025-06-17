using JasperFx.Core.Reflection;
using Marten.Linq;

namespace Catalog_API.Features.Products.GetProducts
{
    public record GetProductsQuery(Guid? CategoryId) : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetProductsHandler> _logger;

        public GetProductsHandler(IDocumentSession documentSession, ILogger<GetProductsHandler> logger)
        {
            _documentSession = documentSession;
            _logger = logger;
        }

        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get products query handle called {@query}", "");

            var query = _documentSession.Query<Product>();

            var filtered = request.CategoryId.HasValue
                ? query.Where(p => p.CategoryId == request.CategoryId.Value)
                : query;

            var products = await filtered.ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
