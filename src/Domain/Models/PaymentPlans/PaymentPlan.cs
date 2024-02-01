namespace Domain.Models.PaymentPlans
{
    public class PaymentPlan : Entity
    {
        public PaymentPlan() : base() { }
        public PaymentPlan(PaymentMethod paymentMethod, decimal minInstallmentValue, List<PaymentPlanInstalment> instalments)
        {
            PaymentMethod = paymentMethod;
            MinInstallmentValue = minInstallmentValue;
            Instalments = instalments;
        }
        public PaymentMethod PaymentMethod { get; private set; }
        public decimal MinInstallmentValue { get; private set; }
        public List<PaymentPlanInstalment> Instalments { get; private set; } = new List<PaymentPlanInstalment>();
    }
}
