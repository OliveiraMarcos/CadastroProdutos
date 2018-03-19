using Domain.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValidatorViewModel
{
    public class CustomerValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo produto deve ser informado")
                .MaximumLength(150).WithMessage("Quantidade máxima de 150 caractérs.");
        }
    }
}
