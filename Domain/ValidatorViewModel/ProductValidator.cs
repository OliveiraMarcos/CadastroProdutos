using Domain.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValidatorViewModel
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("O campo nome deve ser informado")
                .MaximumLength(60).WithMessage("Quantidade máxima de 60 caractérs.");
        }
    }
}
