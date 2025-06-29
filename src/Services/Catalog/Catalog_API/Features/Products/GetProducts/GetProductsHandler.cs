using JasperFx.Core.Reflection;
using Marten.Linq;
using Marten.Pagination;

namespace Catalog_API.Features.Products.GetProducts
{
    public record GetProductsQuery(Guid? CategoryId, int? PageNumber = 1, int? PageSize = 10, bool IsGetAll = false) : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products, long TotalItem, bool HasNextPage);

    internal class GetProductsHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly IDocumentSession _documentSession;

        public GetProductsHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _documentSession.Query<Product>();

            var filtered = request.CategoryId.HasValue
                ? query.Where(p => p.CategoryId == request.CategoryId.Value)
                : query;

            IEnumerable<Product> products = new List<Product>();
            long total = 0;
            bool hasNextPage;
            if (request.IsGetAll)
            {
                products = await filtered.ToListAsync(cancellationToken);
                total = filtered.Count();
                return new GetProductsResult(products, total, false);
            }

            var productPages = await filtered.ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);
            total = productPages.TotalItemCount;
            hasNextPage = productPages.HasNextPage;

            return new GetProductsResult(productPages, total, hasNextPage);
        }
    }
}
