using Domain.Misc;
using Domain.Models.PaymentPlans;

namespace Domain.Interfaces.Services
{
    public interface IPersonPaymentPlanService
    {
        public ServiceResult<PaymentPlan> AddPaymentPlan(Guid personId, Guid paymentPlanId);

        public ServiceResult<PaymentPlan> RemovePaymentPlan(Guid personId, Guid paymentPlanId);
    }
}
