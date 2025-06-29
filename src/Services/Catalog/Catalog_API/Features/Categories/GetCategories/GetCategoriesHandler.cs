namespace Catalog_API.Features.Categories.GetCategories
{
    public record GetCategoriesQuery() : IQuery<GetCategoriesResult>;

    public record GetCategoriesResult(IEnumerable<Category> Categories);

    public class GetCategoriesHandler : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        private readonly IDocumentSession _documentSession;

        public GetCategoriesHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _documentSession.Query<Category>().ToListAsync(cancellationToken);
            return new GetCategoriesResult(categories);
        }
    }
}
