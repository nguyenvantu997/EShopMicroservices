namespace Catalog_API.Features.Categories.GetCategories
{
    public record GetCategoriesQuery() : IQuery<GetCategoriesResult>;

    public record GetCategoriesResult(IEnumerable<Category> Categories);

    public class GetCategoriesHandler : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetCategoriesHandler> _logger;

        public GetCategoriesHandler(IDocumentSession documentSession, ILogger<GetCategoriesHandler> logger)
        {
            _documentSession = documentSession;
            _logger = logger;
        }
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get categories query handle called {@query}", request);

            var categories = await _documentSession.Query<Category>().ToListAsync(cancellationToken);
            return new GetCategoriesResult(categories);
        }
    }
}
