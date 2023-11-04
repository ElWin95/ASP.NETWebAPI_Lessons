using FluentValidation;

namespace ShopAppP416.Dtos.AccountDtos
{
    public class LoginDto
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.UserNameOrEmail)
                .NotEmpty()
                .WithMessage("not empty");
            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("not empty")
                .MinimumLength(8)
                .WithMessage("8-den kichik ola bilmez")
                .MaximumLength(50)
                .WithMessage("50-den boyuk ola bilmez");
        }
    }
}
