using Domain.Models.PaymentPlans;

namespace Domain.Models.People
{
    public class Supplier
    {
        public Supplier() { }
        public Supplier(string businessDescription, List<PaymentPlan> paymentPlans)
        {
            BusinessDescription = businessDescription;
            PaymentPlans = paymentPlans;
        }
        public string BusinessDescription { get; private set; } = string.Empty;
        public List<PaymentPlan> PaymentPlans { get; private set; } = new List<PaymentPlan>();
    }
}
