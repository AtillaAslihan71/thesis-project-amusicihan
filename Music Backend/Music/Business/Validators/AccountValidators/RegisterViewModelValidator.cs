using System;
using System.Collections.Generic;
using System.Text;
using Business.ViewModels.AccountViewModels;
using FluentValidation;

namespace Business.Validators.AccountValidators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(u => u.Name).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Please enter a name !");
            RuleFor(u => u.Surname).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Please enter a surname !");
            RuleFor(u => u.Username).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Please enter a username !");
            RuleFor(u => u.Email).NotNull().NotEmpty().MaximumLength(255).EmailAddress()
                .WithMessage("Please enter a email !");
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Please enter a password !");
        }
    }
}
