using Domain.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValidatorViewModel
{
    public class OrderValidator : AbstractValidator<OrderViewModel>
    {
        public OrderValidator()
        {
            RuleFor(x => x.DeliveryDate)
                .NotEmpty().WithMessage("Informe a data de entrega")
                .Must(EntregaMaiorQueHoje).WithMessage("O campo Entrega deve ser uma data maior que hoje");
            RuleFor(x => x.Observation)
                .MaximumLength(500).WithMessage("Tamanho máximo de 500 caractéres.");
        }
        private static bool EntregaMaiorQueHoje(System.DateTime dataEntrega)
        {
            return dataEntrega >= System.DateTime.Now.AddDays(15);
        }
    }
}
