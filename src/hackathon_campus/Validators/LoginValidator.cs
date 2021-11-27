using FluentValidation;
using hackathon_campus.Core.ViewModels;

namespace GeekStream.Web.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Адрес электронной почты обязателен для заполнения")
                .EmailAddress()
                .WithMessage("Введите корректный адрес электронной почты");
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Пароль необходимо заполнить");
        }
    }
}