using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace FinanceService.Application.Features.Favorite.Add
{
    public class AddFavoriteValidator : AbstractValidator<AddFavoriteCommand>
    {
        public AddFavoriteValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Не верный пользователь");

            RuleFor(x => x.CurrencyName)
                .NotEmpty().WithMessage("Название валюты не может быть пустым")
                .MaximumLength(10).WithMessage("Максимум 50 символов");
        }
    }
}
