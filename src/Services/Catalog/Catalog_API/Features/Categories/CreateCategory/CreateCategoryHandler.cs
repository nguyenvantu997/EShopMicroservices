namespace Catalog_API.Features.Categories.CreateCategory
{
    public record CreateCategoryCommand(string Name, string Description) : ICommand<CreateCategoryResult>;
    public record CreateCategoryResult(Guid Id);
    public class CreateCategoryHandler : ICommandHanlder<CreateCategoryCommand, CreateCategoryResult>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<CreateCategoryHandler> _logger;

        public CreateCategoryHandler(IDocumentSession documentSession, ILogger<CreateCategoryHandler> logger)
        {
            _documentSession = documentSession;
            _logger = logger;
        }

        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create category handle called....");

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            _documentSession.Store(category);
            await _documentSession.SaveChangesAsync();

            return new CreateCategoryResult(category.Id);
        }
    }
}
