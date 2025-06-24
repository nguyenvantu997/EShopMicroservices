namespace Catalog_API.Features.Categories.CreateCategory
{
    public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator() { 
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
