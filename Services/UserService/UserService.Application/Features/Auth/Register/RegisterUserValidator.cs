using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace UserService.Application.Features.Auth.Register
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не може быть пустым");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль не может быть пустым")
                .MinimumLength(6).WithMessage("Пароль не может быть меньше 6 символом");
        }
    }
}
