using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ShopAppP416.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
    }
    public class CategoryCreateDtoValidator: AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("name bosh ola bilmez")
                .MaximumLength(50)
                .WithMessage("50-den boyuk name olmaz");
        }
    }
}
