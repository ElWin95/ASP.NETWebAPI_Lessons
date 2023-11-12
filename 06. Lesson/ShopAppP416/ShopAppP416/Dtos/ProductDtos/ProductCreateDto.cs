using FluentValidation;

namespace ShopAppP416.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; }
    }
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("bosh qoyma")
                .MaximumLength(50).WithMessage("50-den yuxari ola bilmez");

            RuleFor(p => p.SalePrice)
                .NotEmpty()
                .WithMessage("bosh qoyma")
                .GreaterThan(50).WithMessage("price 50-den yuxari olmalidir");
            RuleFor(p => p.CostPrice)
                .NotEmpty()
                .WithMessage("bosh qoyma")
                .GreaterThan(50).WithMessage("price 50-den yuxari olmalidir");
            RuleFor(p => p).Custom((p, context) =>
            {
                if (p.SalePrice <= p.CostPrice)
                {
                    context.AddFailure("SalePrice", "salePrice costPrice-dan kichik ola bilmez..");
                }
            });
        }
    }
}
