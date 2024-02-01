using FluentValidation;
using Domain.Models.PaymentPlans;

namespace Domain.Interfaces.Validations
{
    public interface IPaymentPlanValidation : IValidator<PaymentPlan>
    {
    }
}
