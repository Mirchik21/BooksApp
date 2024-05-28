﻿using FluentValidation;

namespace Application.MediatR.Accounts.Commands
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Поле является обязательным к заполнению");
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Поле является обязательным к заполнению");
        }
    }
}