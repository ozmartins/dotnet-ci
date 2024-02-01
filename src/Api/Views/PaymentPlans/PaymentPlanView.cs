using Domain.Models.PaymentPlans;

namespace Api.Views.PaymentPlans
{
    public class PaymentPlanView : View
    {
        public PaymentMethod PaymentMethod { get; set; }

        public decimal MinInstallmentValue { get; set; }

        public List<PaymentPlanInstalmentView> Instalments { get; set; } = new List<PaymentPlanInstalmentView>();
    }
}
