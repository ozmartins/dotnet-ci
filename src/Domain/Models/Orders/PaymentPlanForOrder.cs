using Domain.Models.PaymentPlans;

namespace Domain.Models.Orders
{
    public class PaymentPlanForOrder
    {
        public PaymentPlanForOrder() { }
        public PaymentPlanForOrder(Guid id, PaymentMethod paymentMethod, int installments, decimal fee)
        {
            Id = id;
            PaymentMethod = paymentMethod;
            Installments = installments;
            Fee = fee;
        }
        public Guid Id { get; set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public int Installments { get; private set; }
        public decimal Fee { get; private set; }
    }
}
