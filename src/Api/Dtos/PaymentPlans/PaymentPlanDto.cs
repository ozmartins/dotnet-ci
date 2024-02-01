using Domain.Models.PaymentPlans;

namespace Api.Dtos.PaymentPlans
{
    public class PaymentPlanDto
    {
        public PaymentMethod PaymentMethod { get; set; }

        public decimal MinInstallmentValue { get; set; }

        public List<PaymentPlanInstalmentDto> Instalments { get; set; } = new List<PaymentPlanInstalmentDto>();
    }
}
