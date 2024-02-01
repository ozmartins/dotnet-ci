namespace Domain.Models.PaymentPlans
{
    public class PaymentPlanInstalment : Entity
    {
        public PaymentPlanInstalment() : base() { }
        public PaymentPlanInstalment(int quantity, decimal fee)
        {
            Quantity = quantity;
            Fee = fee;
        }
        public int Quantity { get; private set; }
        public decimal Fee { get; private set; }
    }
}