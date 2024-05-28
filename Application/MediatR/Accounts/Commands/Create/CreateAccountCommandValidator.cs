using FluentValidation;

namespace Application.MediatR.Accounts.Commands
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(v => v.FirstName)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению");
            RuleFor(v => v.LastName)
             .NotEmpty().WithMessage("Поле является обязательным к заполнению");
            RuleFor(v => v.MiddleName)
             .NotEmpty().WithMessage("Поле является обязательным к заполнению");
            RuleFor(v => v.Email)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению")
               .EmailAddress().WithMessage("Недействительная почта");
            RuleFor(v => v.Password)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению")
               .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов");
            RuleFor(c => c.ConfirmPassword)
               .NotEmpty().WithMessage("Поле является обязательным к заполнению")
                .Equal(c => c.Password).WithMessage("Пароли не совпадают")
               .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов");
        }
    }
}