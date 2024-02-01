using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.PaymentPlans;

namespace Domain.Validations
{
    public class PaymentPlanValidation : AbstractValidator<PaymentPlan>, IPaymentPlanValidation
    {
        public PaymentPlanValidation()
        {
            RuleFor(p => p.PaymentMethod).IsInEnum().WithMessage("O campo 'Método de pagamento' está com um valor inválido.");

            RuleFor(p => p.MinInstallmentValue).GreaterThanOrEqualTo(0).WithMessage("O campo 'Valor mínimo da prestação' não pode ser negativo.");

            RuleFor(p => quantityDuplicated(p)).Equal(false).WithMessage("O método de pagamento possui quantidades repetidas.");

            RuleForEach(p => p.Instalments).ChildRules(instalment => instalment.RuleFor(x => x.Fee).GreaterThanOrEqualTo(0).WithMessage("A taxa informada na prestação não pode ser negativa."));

            RuleForEach(p => p.Instalments).ChildRules(instalment => instalment.RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("A quantidade informada na prestação não pode ser negativa."));
        }

        private bool quantityDuplicated(PaymentPlan paymentPlan)
        {
            var result = paymentPlan.Instalments
                .GroupBy(x => x.Quantity)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            return result.Count() > 0;
        }
    }
}
