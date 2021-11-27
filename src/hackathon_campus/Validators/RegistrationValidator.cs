using FluentValidation;
using hackathon_campus.Core.ViewModels;

namespace GeekStream.Web.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Адрес электронной почты обязателен для заполнения")
                .EmailAddress()
                .WithMessage("Введите корректный адрес электронной почты");
            RuleFor(x => x.UserName)
                .NotNull()
                .WithMessage("Обязательно придумайте свое пользовательское имя");
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("Имя необходимо заполнить")
                .Length(1, 50)
                .WithMessage("Длина имени должа быть от 1 до 50");
            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("Фамилию необходимо заполнить")
                .Length(1, 150)
                .WithMessage("Длина фамилии должна быть от 1 до 150");
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Пароль необходимо заполнить");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Пароли должны совпадать");
        }
    }
}