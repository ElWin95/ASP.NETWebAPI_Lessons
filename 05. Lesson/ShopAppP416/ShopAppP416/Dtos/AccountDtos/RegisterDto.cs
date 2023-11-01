using FluentValidation;

namespace ShopAppP416.Dtos.AccountDtos
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.FullName)
                .NotEmpty()
                .WithMessage("not empty")
                .MaximumLength(50)
                .WithMessage("50den boyuk ola bilmez");
            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage("not empty")
                .MaximumLength(50)
                .WithMessage("50den boyuk ola bilmez");
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("not empty")
                .EmailAddress()
                .WithMessage("A valid email address is required.");
            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("not empty")
                .MinimumLength(8)
                .WithMessage("8den kichik ola bilmez")
                .MaximumLength(50)
                .WithMessage("50den boyuk ola bilmez");
            RuleFor(r => r.RepeatPassword)
                .NotEmpty()
                .WithMessage("not empty")
                .MinimumLength(8)
                .WithMessage("8den kichik ola bilmez")
                .MaximumLength(50)
                .WithMessage("50den boyuk ola bilmez");

            RuleFor(r => r)
                .Custom((r, context) =>
                {
                    if (r.Password != r.RepeatPassword)
                    {
                        context.AddFailure("RepeatPassword", "eyni olmalidir");
                    }
                });
        }
    }
}
