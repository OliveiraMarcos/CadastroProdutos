using Domain.ViewModels;
using FluentValidation;

namespace Domain.ValidatorViewModel
{
    public class OrderItemValidator : AbstractValidator<OrderItemViewModel>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.Quantity)
                .Must(IntMaiorQueZero).WithMessage("O campo Quantidade deve ser um inteiro maior que zero");
            RuleFor(x => x.UnitPrice)
                .Must(DecimalMaiorQueZero).WithMessage("O campo Preço deve ser um decimal maior que zero");
            RuleFor(x => x.ProductId)
                .Must(IntMaiorQueZero).WithMessage("O campo Produto deve ser preenchido");
        }
        private static bool IntMaiorQueZero(int qtd)
        {
            return qtd > 0;
        }
        private static bool DecimalMaiorQueZero(decimal value)
        {
            return value > 0;
        }
    }
}
